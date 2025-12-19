using Main.Application.Dtos.Teachers;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Application.Features.Teachers.Commands.CreateTeacher;
using Main.Application.Features.Teachers.Commands.DeleteTeacher;
using Main.Application.Features.Teachers.Commands.UpdateTeacher;
using Main.Application.Features.Teachers.Queries.GetTeacher;
using Main.Application.Features.Teachers.Queries.GetTeachers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TeachersController : GenericController
    {
        #region constructor
        public TeachersController(IMediator mediator) : base(mediator)
        { }
        #endregion

        #region Get Teacher
        [HttpGet("{id}", Name = "GetTeacher")]
        [ProducesResponseType(typeof(TeacherDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(long id)
        {
            var query = new GetTeacherQuery(id);
            var teacher = await _mediator.Send(query);
            return Ok(teacher);
        }
        #endregion

        #region Get Teachers
        [HttpGet(Name = "GetTeachers")]
        [ProducesResponseType(typeof(IReadOnlyList<TeacherDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<TeacherDto>>> GetTeachers(long id)
        {
            var query = new GetTeachersQuery();
            var teachers = await _mediator.Send(query);
            return Ok(teachers);
        }
        #endregion

        #region Create Teacher
        [HttpPost(Name = "CreateTeacher")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateTeacher([FromBody] CreateTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion

        #region Update Teacher
        [HttpPut(Name = "UpdateTeacher")]
        public async Task<ActionResult> UpdateTeacher([FromBody] UpdateTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion

        #region Delete Teacher
        [HttpDelete("{id}", Name = "DeleteTeacher")]
        public async Task<ActionResult> DeleteTeacher(long id)
        {
            var result = await _mediator.Send(new DeleteTeacherCommand(id));
            return Ok(result);
        }
        #endregion
    }
}
