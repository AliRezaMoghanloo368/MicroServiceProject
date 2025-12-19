using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Commands.DeleteTeacher
{
    public record DeleteTeacherCommand(long Id) : IRequest<Result<bool>>
    {

    }
}
