using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Courses;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Queries.GetCourse
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, Result<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCourseQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var st = await _courseRepository.GetByIdAsync(request.Id);
            var course = _mapper.Map<CourseDto>(st);
            return Result<CourseDto>.SuccessResult(course);
        }
    }
}
