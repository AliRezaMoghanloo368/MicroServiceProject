using Main.Application.Dtos.Students;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Queries.GetStudents
{
    public class GetStudentsQuery : IRequest<Result<IReadOnlyList<StudentDto>>>
    {

    }
}
