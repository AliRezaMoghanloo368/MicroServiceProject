using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Students.Commands.CreateStudent;
using Main.Application.Features.Teachers.Commands.DeleteTeacher;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Result<Teacher>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<CreateTeacherCommand> _logger;
        private readonly IMapper _mapper;
        public CreateTeacherCommandHandler(ITeacherRepository teacherRepository,
            ILogger<CreateTeacherCommand> logger,
            IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<Teacher>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var valid = new CreateTeacherCommandValidation();
            var isValid = await valid.ValidateAsync(request);
            if (!isValid.IsValid)
            {
                return Result<Teacher>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var teacherEntity = _mapper.Map<Teacher>(request);
            var newTeacher = await _teacherRepository.AddAsync(teacherEntity);

            _logger.LogInformation($"Teacher {newTeacher.Id} is successfully created.");
            return Result<Teacher>.SuccessResult(newTeacher, "عملیات ثبت جدید انجام شد.");
        }
    }
}
