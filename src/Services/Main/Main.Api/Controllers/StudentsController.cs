using AutoMapper;
using Main.Api.Grpc.Services;
using Main.Application.Dtos.Histories;
using Main.Application.Dtos.Students;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Application.Features.Students.Queries.GetStudent;
using Main.Application.Features.Students.Queries.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static SharedLibrary.Utilities.Enums;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class StudentsController : GenericController
    {
        #region constructor
        private readonly IMapper _mapper;
        private readonly Logs_HistoryGrpcService _service;
        public StudentsController(IMediator mediator, Logs_HistoryGrpcService service, IMapper mapper) : base(mediator)
        {
            _service = service;
            _mapper = mapper;
        }
        #endregion

        #region Get Student
        [HttpGet("{id}", Name = "GetStudent")]
        [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<StudentDto>> GetStudentById(long id)
        {
            var query = new GetStudentQuery(id);
            var student = await _mediator.Send(query);

            #region GetHistory
            if (student.Data != null)
            {
                var recordId = student.Data.Id.ToString();

                // gRPC call برای گرفتن همه histories
                var h = await _service.GetHistories("test", "student", recordId);

                var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

                student.Data.Histories.AddRange(histories);
            }
            #endregion

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

            #region GetHistory
            if (students.Data != null && students.Data.Count > 0)
            {
                var recordIds = students.Data.Select(c => c.Id.ToString()).ToList();

                // gRPC call برای گرفتن همه histories
                var h = await _service.GetHistories("test", "student", recordIds[0]);

                var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

                foreach (var studentDto in students.Data)
                {
                    studentDto.Histories.AddRange(histories);
                }
            }
            #endregion

            return Ok(students);
        }
        #endregion

        #region Create Student
        [HttpPost(Name = "CreateStudent")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateStudent([FromBody] CreateStudentCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("student", result.Data.Id.ToString(), HistoryAction.add);

            return Ok(result);
        }
        #endregion

        #region Update Student
        [HttpPut(Name = "UpdateStudent")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("student", command.Id.ToString(), HistoryAction.edit);

            return Ok(result);
        }
        #endregion

        #region Delete Student
        [HttpDelete("{id}", Name = "DeleteStudent")]
        public async Task<ActionResult> DeleteStudent(long id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id));

            // For log
            await _service.CreateHistoryAsync("student", id.ToString(), HistoryAction.delete);

            return Ok(result);
        }
        #endregion
    }
}
