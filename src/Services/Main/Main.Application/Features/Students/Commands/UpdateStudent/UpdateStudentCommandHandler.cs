using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result<bool>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<UpdateStudentCommand> _logger;
        private readonly IMapper _mapper;
        public UpdateStudentCommandHandler(IStudentRepository studentRepository,
            ILogger<UpdateStudentCommand> logger,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var valid = new UpdateStudentCommandValidation(_studentRepository);
            var isValid = await valid.ValidateAsync(request);
            if (!isValid.IsValid)
            {
                _logger.LogError("Student is not valid.");
                return Result<bool>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var studentForUpdate = await _studentRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, studentForUpdate, typeof(UpdateStudentCommand), typeof(Student));

            await _studentRepository.UpdateAsync(studentForUpdate);
            _logger.LogInformation($"Student {studentForUpdate.Id} is successfully updated.");

            return Result<bool>.SuccessResult(true, "عملیات ویرایش انجام شد.");
        }
    }
}
