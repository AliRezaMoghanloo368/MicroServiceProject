using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<Course>>
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

        public async Task<Result<Course>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var valid = new CreateCourseCommandValidation();
            var isValid = await valid.ValidateAsync(request);
            if (!isValid.IsValid)
            {
                return Result<Course>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var courseEntity = _mapper.Map<Course>(request);
            var newCourse = await _courseRepository.AddAsync(courseEntity);

            _logger.LogInformation($"Course {newCourse.Id} is successfully created.");
            return Result<Course>.SuccessResult(newCourse, "عملیات ثبت جدید انجام شد.");
        }
    }
}
