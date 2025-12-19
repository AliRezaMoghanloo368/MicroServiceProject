using AutoMapper;
using FluentValidation;
using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<Student>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CreateStudentCommand> _logger;
        private readonly IMapper _mapper;
        public CreateStudentCommandHandler(IStudentRepository studentRepository,
            ILogger<CreateStudentCommand> logger,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<Student>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var valid = new CreateStudentCommandValidation();
            var isValid = await valid.ValidateAsync(request);
            if (!isValid.IsValid)
            {
                return Result<Student>.ErrorResult(isValid.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var studentEntity = _mapper.Map<Student>(request);
            var newStudent = await _studentRepository.AddAsync(studentEntity);

            _logger.LogInformation($"Student {newStudent.Id} is successfully created.");
            return Result<Student>.SuccessResult(newStudent, "عملیات ثبت جدید انجام شد.");
        }
    }
}
