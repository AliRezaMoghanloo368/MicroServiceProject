using Identity.Domain.Application.Services.Authenticate;
using Identity.Domain.Core.Interfaces;
using Identity.Domain.Infra.Data.Context;
using Identity.Domain.Infra.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLibrary.Encryptor;


namespace Identity.Domain.Infra.IoC
{
    public static class IdentityDependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            //service.AddAutoMapper(typeof(IdentityMappingProfiles).Assembly);

            #region Data Layer
            service.AddScoped<IUserRepository, UserRepository>();
            if (env.IsEnvironment("Testing"))
            {
                service.AddDbContext<IdentityContext>(opts =>
                    opts.UseInMemoryDatabase("InMemoryIdentityApi"));
            }
            else
            {
                service.AddDbContext<IdentityContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("Root")));
            }
            #endregion

            #region Application Layer
            //service.AddScoped<UserService>();
            service.AddJwt(configuration);
            #endregion

            #region Shared Layer
            service.AddSingleton<IEncryptor, Encryptor>();
            #endregion

            return service;
        }
    }
}
