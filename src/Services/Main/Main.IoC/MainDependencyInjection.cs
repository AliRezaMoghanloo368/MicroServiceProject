using FluentValidation;
using Main.Application.Behaviors;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Application.Features.Students.Queries.GetStudent;
using Main.Application.Features.Students.Queries.GetStudents;
using Main.Application.Mapping;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CreateStudentCommandHandler).Assembly);
            services.AddMediatR(typeof(UpdateStudentCommandHandler).Assembly);
            services.AddMediatR(typeof(DeleteStudentCommandHandler).Assembly);
            services.AddMediatR(typeof(GetStudentQueryHandler).Assembly);
            services.AddMediatR(typeof(GetStudentsQueryHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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
