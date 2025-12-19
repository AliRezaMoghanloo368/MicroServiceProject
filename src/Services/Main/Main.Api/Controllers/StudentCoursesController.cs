using Main.Application.Features.StudentCourses.Commands.CreateStudentCourse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Api.Controllers
{
    [Route("api/v1/student-courses")]
    public class StudentCoursesController : GenericController
    {
        #region constructor
        public StudentCoursesController(IMediator mediator) : base(mediator) { }
        #endregion

        #region addStudentToCourse
        [HttpPost]
        public async Task<IActionResult> AddStudentToCourse(
            [FromBody] CreateStudentCourseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
    }
}
