using Main.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result<bool>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<DeleteStudentCommand> _logger;
        public DeleteStudentCommandHandler(IStudentRepository studentRepository,
            ILogger<DeleteStudentCommand> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studentForDelete = await _studentRepository.GetByIdAsync(request.Id);

            if (studentForDelete == null)
            {
                _logger.LogError("student not exists.");
                return Result<bool>.ErrorResult("دانش آموزی یافت نشد.");
                //throw new NotFoundException(nameof(Student), request.Id);
            }
            else
            {
                await _studentRepository.InActiveAsync(studentForDelete.Id);
                _logger.LogInformation($"student {studentForDelete.Id} is successfully deleted (inactive).");
            }

            return Result<bool>.SuccessResult(true, "عملیات حذف انجام شد.");
        }
    }
}
