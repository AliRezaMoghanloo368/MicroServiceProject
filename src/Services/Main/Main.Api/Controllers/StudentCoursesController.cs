using Main.Api.Grpc.Services;
using Main.Application.Features.StudentCourses.Commands.CreateStudentCourse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static SharedLibrary.Utilities.Enums;

namespace Main.Api.Controllers
{
    [Route("api/v1/student-courses")]
    public class StudentCoursesController : GenericController
    {
        #region constructor
        private readonly Logs_HistoryGrpcService _service;
        public StudentCoursesController(IMediator mediator, Logs_HistoryGrpcService service) : base(mediator)
        {
            _service = service;
        }
        #endregion

        #region addStudentToCourse
        [HttpPost]
        public async Task<IActionResult> AddStudentToCourse(
            [FromBody] CreateStudentCourseCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("student-course", command.CourseId.ToString(), HistoryAction.add);

            return Ok(result);
        }
        #endregion
    }
}
