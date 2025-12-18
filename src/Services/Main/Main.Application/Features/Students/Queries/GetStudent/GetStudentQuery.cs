using Main.Application.Dtos.Students;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Queries.GetStudent
{
    public record GetStudentQuery(int Id) : IRequest<Result<StudentDto>>
    {

    }
}
