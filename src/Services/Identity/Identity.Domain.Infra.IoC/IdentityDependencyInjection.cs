using Identity.Domain.Application.Services.Authenticate.Implement;
using Identity.Domain.Application.Services.Authenticate.Interfaces;
using SharedLibrary.Encryptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Infra.IoC
{
    public static class IdentityDependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddAutoMapper(typeof(IdentityMappingProfiles).Assembly);

            #region Data Layer
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddDbContext<THRContext>(option =>
                                option.UseSqlServer(configuration.GetConnectionString("Root")));
            #endregion

            #region Application Layer
            service.AddScoped<IJwtHandler, JwtHandler>();
            service.AddScoped<UserService>();
            service.AddJwt(configuration);
            #endregion

            #region Shared Layer
            service.AddSingleton<IEncryptor, Encryptor>();
            #endregion

            return service;
        }
    }
}
