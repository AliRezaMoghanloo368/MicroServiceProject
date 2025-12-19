using Main.Application.Dtos.Courses;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQuery : IRequest<Result<List<CourseDto>>>
    {
    }
}
