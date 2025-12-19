using Main.Application.Dtos.Courses;
using Main.Application.Features.Courses.Commands.CreateCourse;
using Main.Application.Features.Courses.Commands.DeleteCourse;
using Main.Application.Features.Courses.Commands.UpdateCourse;
using Main.Application.Features.Courses.Queries.GetCourse;
using Main.Application.Features.Courses.Queries.GetCourses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CoursesController : GenericController
    {
        #region constructor
        public CoursesController(IMediator mediator) : base(mediator)
        { }
        #endregion

        #region Get Course
        [HttpGet("{id}", Name = "GetCourse")]
        [ProducesResponseType(typeof(CourseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CourseDto>> GetCourseById(long id)
        {
            var query = new GetCourseQuery(id);
            var course = await _mediator.Send(query);
            return Ok(course);
        }
        #endregion

        #region Get Courses
        [HttpGet(Name = "GetCourses")]
        [ProducesResponseType(typeof(IReadOnlyList<CourseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetCourses(long id)
        {
            var query = new GetCoursesQuery();
            var courses = await _mediator.Send(query);
            return Ok(courses);
        }
        #endregion

        #region Create Course
        [HttpPost(Name = "CreateCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateCourse([FromBody] CreateCourseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion

        #region Update Course
        [HttpPut(Name = "UpdateCourse")]
        public async Task<ActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion

        #region Delete Course
        [HttpDelete("{id}", Name = "DeleteCourse")]
        public async Task<ActionResult> DeleteCourse(long id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand(id));
            return Ok(result);
        }
        #endregion
    }
}
