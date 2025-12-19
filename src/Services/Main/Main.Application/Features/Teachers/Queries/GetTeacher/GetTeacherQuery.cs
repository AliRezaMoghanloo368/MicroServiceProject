using Main.Application.Dtos.Teachers;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Queries.GetTeacher
{
    public record GetTeacherQuery(long Id) : IRequest<Result<TeacherDto>>
    {
    }
}
