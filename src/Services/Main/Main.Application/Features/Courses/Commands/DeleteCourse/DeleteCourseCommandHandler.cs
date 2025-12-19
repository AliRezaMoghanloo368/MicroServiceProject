using Main.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result<bool>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<DeleteCourseCommand> _logger;
        public DeleteCourseCommandHandler(ICourseRepository courseRepository,
            ILogger<DeleteCourseCommand> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var courseForDelete = await _courseRepository.GetByIdAsync(request.Id);

            if (courseForDelete == null)
            {
                _logger.LogError("course not exists.");
                return Result<bool>.ErrorResult("درسی یافت نشد.");
                //throw new NotFoundException(nameof(Course), request.Id);
            }
            else
            {
                await _courseRepository.DeleteAsync(courseForDelete);
                _logger.LogInformation($"Course {courseForDelete.Id} is successfully deleted (inactive).");
            }

            return Result<bool>.SuccessResult(true, "عملیات حذف انجام شد.");
        }
    }
}
