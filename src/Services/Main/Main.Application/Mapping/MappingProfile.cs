using AutoMapper;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
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
            #endregion

            #region Teacher
            //CreateMap<Teacher, CreateTeacherCommand>().ReverseMap();
            //CreateMap<Teacher, UpdateTeacherCommand>().ReverseMap();
            #endregion

            #region Course
            //CreateMap<Course, CreateCourseCommand>().ReverseMap();
            //CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            #endregion
        }
    }
}
