using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
