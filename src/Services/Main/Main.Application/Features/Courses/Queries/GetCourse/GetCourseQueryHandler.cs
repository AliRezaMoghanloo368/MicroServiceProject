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
            var course = await _courseRepository.GetByIdAsync(request.Id);
            await _courseRepository.LoadTeacherAsync(course);
            await _courseRepository.LoadStudentCoursesAsync(course);
            var courseDto = _mapper.Map<CourseDto>(course);
            return Result<CourseDto>.SuccessResult(courseDto);
        }
    }
}
