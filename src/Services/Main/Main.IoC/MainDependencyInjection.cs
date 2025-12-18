using Main.Application.Contracts.Persistence;
using Main.Infrastructure.Persistence;
using Main.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Main.IoC
{
    public static class MainDependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            #region Application Layer
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Data Layer
            services.AddDbContext<MainContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Root"));
            });
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            #endregion

            #region Shared
            //service.AddScoped<IEncrypter, Encrypter>();
            #endregion

            return services;
        }
    }
}
