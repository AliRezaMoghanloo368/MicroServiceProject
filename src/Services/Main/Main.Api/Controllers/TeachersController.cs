using AutoMapper;
using Main.Api.Grpc.Services;
using Main.Application.Dtos.Histories;
using Main.Application.Dtos.Teachers;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Application.Features.Teachers.Commands.CreateTeacher;
using Main.Application.Features.Teachers.Commands.DeleteTeacher;
using Main.Application.Features.Teachers.Commands.UpdateTeacher;
using Main.Application.Features.Teachers.Queries.GetTeacher;
using Main.Application.Features.Teachers.Queries.GetTeachers;
using Main.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static SharedLibrary.Utilities.Enums;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TeachersController : GenericController
    {
        #region constructor
        private readonly IMapper _mapper;
        private readonly Logs_HistoryGrpcService _service;
        public TeachersController(IMediator mediator, Logs_HistoryGrpcService service, IMapper mapper) : base(mediator)
        {
            _service = service;
            _mapper = mapper;
        }
        #endregion

        #region Get Teacher
        [HttpGet("{id}", Name = "GetTeacher")]
        [ProducesResponseType(typeof(TeacherDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(long id)
        {
            var query = new GetTeacherQuery(id);
            var teacher = await _mediator.Send(query);

            #region GetHistory
            if (teacher.Data != null)
            {
                var recordId = teacher.Data.Id.ToString();

                // gRPC call برای گرفتن همه histories
                var h = await _service.GetHistories("test", "teacher", recordId);

                var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

                teacher.Data.Histories.AddRange(histories);
            }
            #endregion

            return Ok(teacher);
        }
        #endregion

        #region Get Teachers
        [HttpGet(Name = "GetTeachers")]
        [ProducesResponseType(typeof(IReadOnlyList<TeacherDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<TeacherDto>>> GetTeachers()
        {
            var query = new GetTeachersQuery();
            var teachers = await _mediator.Send(query);

            #region GetHistory
            if (teachers.Data != null && teachers.Data.Count > 0)
            {
                var recordIds = teachers.Data.Select(c => c.Id.ToString()).ToList();

                // gRPC call برای گرفتن همه histories
                var h = await _service.GetHistories("test", "teacher", recordIds[0]);

                var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

                foreach (var teacherDto in teachers.Data)
                {
                    teacherDto.Histories.AddRange(histories);
                }
            }
            #endregion

            return Ok(teachers);
        }
        #endregion

        #region Create Teacher
        [HttpPost(Name = "CreateTeacher")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateTeacher([FromBody] CreateTeacherCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("teacher", result.Data.Id.ToString(), HistoryAction.add);

            return Ok(result);
        }
        #endregion

        #region Update Teacher
        [HttpPut(Name = "UpdateTeacher")]
        public async Task<ActionResult> UpdateTeacher([FromBody] UpdateTeacherCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("teacher", command.Id.ToString(), HistoryAction.edit);

            return Ok(result);
        }
        #endregion

        #region Delete Teacher
        [HttpDelete("{id}", Name = "DeleteTeacher")]
        public async Task<ActionResult> DeleteTeacher(long id)
        {
            var result = await _mediator.Send(new DeleteTeacherCommand(id));

            // For log
            await _service.CreateHistoryAsync("teacher", id.ToString(), HistoryAction.delete);

            return Ok(result);
        }
        #endregion
    }
}
