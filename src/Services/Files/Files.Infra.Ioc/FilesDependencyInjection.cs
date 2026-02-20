using Files.Application.Interfaces;
using Files.Application.Mapping;
using Files.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Infra.IoC
{
    public static class FilesDependencyInjection
    {
        public static IServiceCollection FilesRegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            #region Application Layer
            services.AddScoped<IFilesRepository, FilesRepository>();
            #endregion

            #region Other
            services.AddAutoMapper(typeof(FilesMappingProfile).Assembly);
            #endregion

            return services;
        }
    }
}
