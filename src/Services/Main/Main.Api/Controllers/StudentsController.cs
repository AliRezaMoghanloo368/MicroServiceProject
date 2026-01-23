using AutoMapper;
using EventBus.Messages.Events;
using Logs.Domain.Models;
using Main.Api.Grpc.Services;
using Main.Application.Dtos.Histories;
using Main.Application.Dtos.Students;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Application.Features.Students.Queries.GetStudent;
using Main.Application.Features.Students.Queries.GetStudents;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using static SharedLibrary.Utilities.Enums;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class StudentsController : GenericController
    {
        #region constructor
        private readonly IMapper _mapper;
        private readonly Logs_HistoryGrpcService _grpService;
        private readonly IPublishEndpoint _publisher;
        public StudentsController(IMediator mediator, Logs_HistoryGrpcService service, IMapper mapper
            , IPublishEndpoint publisher) : base(mediator)
        {
            _grpService = service;
            _mapper = mapper;
            _publisher = publisher;
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
                var h = await _grpService.GetHistories("test", "student", recordId);

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
                int i = 0;
                foreach (var studentDto in students.Data)
                {
                    var recordIds = students.Data.Select(c => c.Id).ToList();

                    // gRPC call برای گرفتن همه histories
                    var h = await _grpService.GetHistories("test", "student", recordIds[i++].ToString());

                    var histories = _mapper.Map<List<HistoryDto>>(h.Histories);
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
            await _grpService.CreateHistoryAsync("student", result.Data.Id.ToString(), HistoryAction.add);

            return Ok(result);
        }
        #endregion

        #region Update Student
        [HttpPut(Name = "UpdateStudent")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _grpService.CreateHistoryAsync("student", command.Id.ToString(), HistoryAction.edit);

            return Ok(result);
        }
        #endregion

        #region Delete Student
        [HttpDelete("{id}", Name = "DeleteStudent")]
        public async Task<ActionResult> DeleteStudent(long id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id));

            // For log
            await _grpService.CreateHistoryAsync("student", id.ToString(), HistoryAction.delete);

            return Ok(result);
        }
        #endregion

        #region Publish
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Publish([FromBody] LogsHistoryPublish command)
        {
            ////get existing...

            //create event
            var eventMessage = _mapper.Map<LogsHistoryEvent>(command);

            //send event to rabbitmq
            await _publisher.Publish(eventMessage);

            return Accepted();
        }
        #endregion
    }
}
