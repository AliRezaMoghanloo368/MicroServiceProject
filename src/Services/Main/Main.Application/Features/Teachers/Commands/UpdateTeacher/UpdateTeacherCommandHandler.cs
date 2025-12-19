using AutoMapper;
using FluentValidation;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Students.Commands.UpdateStudent;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Result<bool>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<UpdateTeacherCommand> _logger;
        private readonly IMapper _mapper;
        public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository,
            ILogger<UpdateTeacherCommand> logger,
            IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var valid = new UpdateTeacherCommandValidation(_teacherRepository);
            var isValid = await valid.ValidateAsync(request);
            if (!isValid.IsValid)
            {
                _logger.LogError("Student is not valid.");
                return Result<bool>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var teacherForUpdate = await _teacherRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, teacherForUpdate, typeof(UpdateTeacherCommand), typeof(Teacher));

            await _teacherRepository.UpdateAsync(teacherForUpdate);
            _logger.LogInformation($"teacher {teacherForUpdate.Id} is successfully updated.");

            return Result<bool>.SuccessResult(true, "عملیات ویرایش انجام شد.");
        }
    }
}
