using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result<bool>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<UpdateCourseCommand> _logger;
        private readonly IMapper _mapper;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository,
            ILogger<UpdateCourseCommand> logger,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var valid = new UpdateCourseCommandValidation(_courseRepository);
            var isValid = await valid.ValidateAsync(request);
            if (!isValid.IsValid)
            {
                _logger.LogError("Course is not valid.");
                return Result<bool>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var courseForUpdate = await _courseRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, courseForUpdate, typeof(UpdateCourseCommand), typeof(Course));

            await _courseRepository.UpdateAsync(courseForUpdate);
            _logger.LogInformation($"Course {courseForUpdate.Id} is successfully updated.");

            return Result<bool>.SuccessResult(true, "عملیات ویرایش انجام شد.");
        }
    }
}
