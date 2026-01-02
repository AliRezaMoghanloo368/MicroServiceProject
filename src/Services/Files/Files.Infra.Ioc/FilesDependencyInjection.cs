using Files.Application.Interfaces;
using Files.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Infra.IoC
{
    public static class FilesDependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            #region Application Layer
            services.AddScoped<IFilesRepository, FilesRepository>();
            #endregion

            #region Other
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(typeof(MappingProfile).Assembly);
            #endregion

            #region Shared
            //service.AddScoped<IEncrypter, Encrypter>();
            #endregion

            return services;
        }
    }
}
