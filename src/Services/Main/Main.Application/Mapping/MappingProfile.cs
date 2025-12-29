using AutoMapper;
using Logs.Grpc.Protos;
using Main.Application.Dtos.Courses;
using Main.Application.Dtos.Histories;
using Main.Application.Dtos.StudentCourses;
using Main.Application.Dtos.Students;
using Main.Application.Dtos.Teachers;
using Main.Application.Features.Courses.Commands.CreateCourse;
using Main.Application.Features.Courses.Commands.UpdateCourse;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Application.Features.Teachers.Commands.CreateTeacher;
using Main.Application.Features.Teachers.Commands.UpdateTeacher;
using Main.Domain.Models;

namespace Main.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Student
            CreateMap<Student, CreateStudentCommand>().ReverseMap();
            CreateMap<Student, UpdateStudentCommand>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            #endregion

            #region Teacher
            CreateMap<Teacher, CreateTeacherCommand>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherCommand>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            #endregion

            #region Course
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseDto>()
                .ForMember(dest => dest.StudentId,
                    opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.StudentFullName,
                    opt => opt.MapFrom(src =>
                        src.Student.FirstName + " " + src.Student.LastName));
            #endregion

            CreateMap<HistoryModel, GetHistoriesResponse>().ReverseMap();
            CreateMap<HistoryDto, CreateHistoryRequest>().ReverseMap();
        }
    }
}
