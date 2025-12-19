using Main.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Commands.DeleteTeacher
{
    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, Result<bool>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<DeleteTeacherCommand> _logger;
        public DeleteTeacherCommandHandler(ITeacherRepository teacherRepository,
            ILogger<DeleteTeacherCommand> logger)
        {
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherForDelete = await _teacherRepository.GetByIdAsync(request.Id);

            if (teacherForDelete == null)
            {
                _logger.LogError("teacher not exists.");
                return Result<bool>.ErrorResult("دانش آموزی یافت نشد.");
                //throw new NotFoundException(nameof(Teacher), request.Id);
            }
            else
            {
                await _teacherRepository.InActiveAsync(teacherForDelete.Id);
                _logger.LogInformation($"teacher {teacherForDelete.Id} is successfully deleted (inactive).");
            }

            return Result<bool>.SuccessResult(true, "عملیات حذف انجام شد.");
        }
    }
}
