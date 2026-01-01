using AutoMapper;
using Main.Api.Grpc.Services;
using Main.Application.Dtos.Courses;
using Main.Application.Dtos.Histories;
using Main.Application.Features.Courses.Commands.CreateCourse;
using Main.Application.Features.Courses.Commands.DeleteCourse;
using Main.Application.Features.Courses.Commands.UpdateCourse;
using Main.Application.Features.Courses.Queries.GetCourse;
using Main.Application.Features.Courses.Queries.GetCourses;
using Main.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static SharedLibrary.Utilities.Enums;

namespace Main.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CoursesController : GenericController
    {
        #region constructor
        private readonly IMapper _mapper;
        private readonly Logs_HistoryGrpcService _service;
        public CoursesController(IMediator mediator, Logs_HistoryGrpcService service, IMapper mapper = null) : base(mediator)
        {
            _service = service;
            _mapper = mapper;
        }
        #endregion

        #region Get Course
        [HttpGet("{id}", Name = "GetCourse")]
        [ProducesResponseType(typeof(CourseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CourseDto>> GetCourseById(long id)
        {
            var query = new GetCourseQuery(id);
            var course = await _mediator.Send(query);

            #region GetHistory
            if (course.Data != null)
            {
                var recordId = course.Data.Id.ToString();

                // gRPC call برای گرفتن همه histories
                var h = await _service.GetHistories("test", "course", recordId);

                var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

                course.Data.Histories.AddRange(histories);
            }
            #endregion

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

            #region GetHistory
            var recordIds = courses.Data.Select(c => c.Id.ToString()).ToList();

            // gRPC call برای گرفتن همه histories
            var h = await _service.GetHistories("test", "course", recordIds[0]);

            var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

            foreach (var courseDto in courses.Data)
            {
                courseDto.Histories.AddRange(histories);
            }
            #endregion

            return Ok(courses);
        }
        #endregion

        #region Create Course
        [HttpPost(Name = "CreateCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateCourse([FromBody] CreateCourseCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("course", result.Data.Id.ToString(), HistoryAction.add);

            return Ok(result);
        }
        #endregion

        #region Update Course
        [HttpPut(Name = "UpdateCourse")]
        public async Task<ActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
        {
            var result = await _mediator.Send(command);

            // For log
            await _service.CreateHistoryAsync("course", command.Id.ToString(), HistoryAction.edit);

            return Ok(result);
        }
        #endregion

        #region Delete Course
        [HttpDelete("{id}", Name = "DeleteCourse")]
        public async Task<ActionResult> DeleteCourse(long id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand(id));

            // For log
            await _service.CreateHistoryAsync("course", id.ToString(), HistoryAction.edit);

            return Ok(result);
        }
        #endregion
    }
}
