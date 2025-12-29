using AutoMapper;
using Main.Api.Grpc.Services;
using Main.Application.Dtos.Courses;
using Main.Application.Dtos.Histories;
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

            var recordIds = courses.Data.Select(c => c.Id.ToString()).ToList();

            // gRPC call برای گرفتن همه histories
            var h = await _service.GetHistories("test", "student", recordIds[0]);

            var histories = _mapper.Map<List<HistoryDto>>(h.Histories);

            foreach (var courseDto in courses.Data)
            {
                courseDto.Histories.AddRange(histories);
            }

            //foreach (var courseDto in courseDtos)
            //{
            //    courseDto.Histories = histories
            //        .Where(h => h.RecordId == courseDto.Id.ToString())
            //        .ToList();
            //}

            return Ok(courses);
            //return Ok(courses);
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
