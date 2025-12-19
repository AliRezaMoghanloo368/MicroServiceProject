using Main.Application.Dtos.Courses;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Queries.GetCourse
{
    public record GetCourseQuery(long Id) : IRequest<Result<CourseDto>>
    {
    }
}
