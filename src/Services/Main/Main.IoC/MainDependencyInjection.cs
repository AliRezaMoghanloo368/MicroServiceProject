using FluentValidation;
using Main.Application.Behaviors;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Courses.Commands.CreateCourse;
using Main.Application.Features.Courses.Commands.DeleteCourse;
using Main.Application.Features.Courses.Commands.UpdateCourse;
using Main.Application.Features.Courses.Queries.GetCourse;
using Main.Application.Features.Courses.Queries.GetCourses;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Application.Features.Students.Queries.GetStudent;
using Main.Application.Features.Students.Queries.GetStudents;
using Main.Application.Features.Teachers.Commands.CreateTeacher;
using Main.Application.Features.Teachers.Commands.DeleteTeacher;
using Main.Application.Features.Teachers.Commands.UpdateTeacher;
using Main.Application.Features.Teachers.Queries.GetTeacher;
using Main.Application.Features.Teachers.Queries.GetTeachers;
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
            #region ------------------------Application Layer
            #region Student Settings
            services.AddMediatR(typeof(CreateStudentCommandHandler).Assembly);
            services.AddMediatR(typeof(UpdateStudentCommandHandler).Assembly);
            services.AddMediatR(typeof(DeleteTeacherCommandHandler).Assembly);
            services.AddMediatR(typeof(GetStudentQueryHandler).Assembly);
            services.AddMediatR(typeof(GetStudentsQueryHandler).Assembly);
            services.AddScoped<IStudentRepository, StudentRepository>();
            #endregion

            #region Teacher Settings
            services.AddMediatR(typeof(CreateTeacherCommandHandler).Assembly);
            services.AddMediatR(typeof(UpdateTeacherCommandHandler).Assembly);
            services.AddMediatR(typeof(DeleteTeacherCommandHandler).Assembly);
            services.AddMediatR(typeof(GetTeacherQueryHandler).Assembly);
            services.AddMediatR(typeof(GetTeachersQueryHandler).Assembly);
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            #endregion
                    
            #region Course Settings
            services.AddMediatR(typeof(CreateCourseCommandHandler).Assembly);
            services.AddMediatR(typeof(UpdateCourseCommandHandler).Assembly);
            services.AddMediatR(typeof(DeleteCourseCommandHandler).Assembly);
            services.AddMediatR(typeof(GetCourseQueryHandler).Assembly);
            services.AddMediatR(typeof(GetCoursesQueryHandler).Assembly);
            services.AddScoped<ICourseRepository, CourseRepository>();
            #endregion

            #region Other
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            #endregion
            #endregion

            #region ------------------------Data Layer
            services.AddDbContext<MainContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Root"));
            });
            #endregion

            #region ------------------------Shared
            //service.AddScoped<IEncrypter, Encrypter>();
            #endregion

            return services;
        }
    }
}
