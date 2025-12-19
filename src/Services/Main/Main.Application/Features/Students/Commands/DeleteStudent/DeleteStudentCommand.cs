using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.DeleteStudent
{
    public record DeleteStudentCommand(long Id) : IRequest<Result<bool>>
    {
    }
}
