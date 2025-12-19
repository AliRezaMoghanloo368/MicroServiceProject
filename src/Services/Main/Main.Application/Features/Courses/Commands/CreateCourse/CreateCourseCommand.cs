using Main.Application.Dtos.Courses;
using Main.Domain.Models;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<Result<CourseDto>>
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public long TeacherId { get; set; }
    }
}
