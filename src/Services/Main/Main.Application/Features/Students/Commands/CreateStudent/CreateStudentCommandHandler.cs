using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Students.Commands.DeleteStudent;
using Main.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result<Student>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<DeleteStudentCommand> _logger;
        private readonly IMapper _mapper;
        public CreateStudentCommandHandler(IStudentRepository studentRepository,
            ILogger<DeleteStudentCommand> logger,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<Student>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentEntity = _mapper.Map<Student>(request);
            var newStudent = await _studentRepository.AddAsync(studentEntity);

            _logger.LogInformation($"Student {newStudent.Id} is successfully created.");
            return Result<Student>.SuccessResult(newStudent, "عملیات ثبت جدید انجام شد.");
        }
    }
}
