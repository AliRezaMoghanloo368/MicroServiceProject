using Main.Application.Dtos.Teachers;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Queries.GetTeachers
{
    public class GetTeachersQuery : IRequest<Result<List<TeacherDto>>>
    {
    }
}
