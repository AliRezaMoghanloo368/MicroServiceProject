using Logs.Grpc.Services;
using Main.Api.Grpc.Services;
using Main.Application.Dtos.Students;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Application.Features.Students.Queries.GetStudent;
using Main.Application.Features.Students.Queries.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class StudentsController : GenericController
    {
        #region constructor
        private readonly Logs_HistoryGrpcService _historyGrpcService;
        public StudentsController(IMediator mediator, Logs_HistoryGrpcService historyGrpcService) : base(mediator)
        {
            _historyGrpcService = historyGrpcService;
        }
        #endregion

        #region Get Student
        [HttpGet("{id}", Name = "GetStudent")]
        [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<StudentDto>> GetStudentById(long id)
        {
            var query = new GetStudentQuery(id);
            var student = await _mediator.Send(query);
            return Ok(student);
        }
        #endregion

        #region Get Students
        [HttpGet(Name = "GetStudents")]
        [ProducesResponseType(typeof(IReadOnlyList<StudentDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<StudentDto>>> GetStudents(long id)
        {
            var query = new GetStudentsQuery();
            var students = await _mediator.Send(query);
            return Ok(students);
        }
        #endregion

        #region Create Student
        [HttpPost(Name = "CreateStudent")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);

            #region For log
            await _historyGrpcService.CreateHistoryAsync("student", result.Data.Id.ToString());
            #endregion

            return Ok(result);
        }
        #endregion

        #region Update Student
        [HttpPut(Name = "UpdateStudent")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion

        #region Delete Student
        [HttpDelete("{id}", Name = "DeleteStudent")]
        public async Task<ActionResult> DeleteStudent(long id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id));
            return Ok(result);
        }
        #endregion
    }
}
