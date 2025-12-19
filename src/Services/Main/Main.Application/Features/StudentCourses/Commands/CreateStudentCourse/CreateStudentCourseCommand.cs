using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.StudentCourses.Commands.CreateStudentCourse
{
    public record CreateStudentCourseCommand(int StudentId, int CourseId) : IRequest<Result<bool>>
    {
    }
}
