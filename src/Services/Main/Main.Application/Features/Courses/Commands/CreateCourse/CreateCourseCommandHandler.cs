using AutoMapper;
using Grpc.Core;
using Logs.Domain.Models;
using Logs.Grpc.Protos;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Courses;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CreateCourseCommand> _logger;
        private readonly IMapper _mapper;
        public CreateCourseCommandHandler(ICourseRepository courseRepository,
            ILogger<CreateCourseCommand> logger,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var valid = new CreateCourseCommandValidation();
                var isValid = await valid.ValidateAsync(request);
                if (!isValid.IsValid)
                {
                    return Result<CourseDto>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
                }

                var courseEntity = _mapper.Map<Course>(request);
                var newCourse = await _courseRepository.AddAsync(courseEntity);

                await _courseRepository.LoadTeacherAsync(newCourse);
                await _courseRepository.LoadStudentCoursesAsync(newCourse);

                var result = _mapper.Map<CourseDto>(newCourse);
                _logger.LogInformation($"Course {result.Id} is successfully created.");
                return Result<CourseDto>.SuccessResult(result, "عملیات ثبت جدید انجام شد.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateCourse");
                throw new Exception(ex.Message);
            }
        }
    }
}
