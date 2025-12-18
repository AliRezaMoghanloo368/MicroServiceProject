using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result<bool>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<DeleteStudentCommand> _logger;
        private readonly IMapper _mapper;
        public UpdateStudentCommandHandler(IStudentRepository studentRepository,
            ILogger<DeleteStudentCommand> logger,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentForUpdate = await _studentRepository.GetByIdAsync(request.Id);
            if (studentForUpdate == null)
            {
                _logger.LogError("order is not exists.");
                return Result<bool>.ErrorResult("دانش آموزی یافت نشد.");
                //throw new NotFoundException(nameof(Student), request.Id);
            }

            _mapper.Map(request, studentForUpdate, typeof(UpdateStudentCommand), typeof(Student));

            await _studentRepository.UpdateAsync(studentForUpdate);
            _logger.LogInformation($"order {studentForUpdate.Id} is successfully updated.");

            return Result<bool>.SuccessResult(true, "عملیات ویرایش انجام شد.");
        }
    }
}
