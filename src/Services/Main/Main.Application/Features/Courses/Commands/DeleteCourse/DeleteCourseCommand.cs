using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.DeleteCourse
{
    public record DeleteCourseCommand(long Id) : IRequest<Result<bool>>
    {
    }
}
