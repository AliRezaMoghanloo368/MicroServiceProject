using Main.Domain.Models;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<Result<Course>>
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
