using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Courses;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, Result<List<CourseDto>>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCoursesQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<CourseDto>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var list = await _courseRepository.GetAllWithTeacherAsync();
            var courseList = _mapper.Map<List<CourseDto>>(list);
            return Result<List<CourseDto>>.SuccessResult(courseList);
        }
    }
}
