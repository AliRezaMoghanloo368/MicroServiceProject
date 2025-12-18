using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Students;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Queries.GetStudent
{
    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, Result<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var st = await _studentRepository.GetByIdAsync(request.Id);
            var student = _mapper.Map<StudentDto>(st);
            return Result<StudentDto>.SuccessResult(student);
        }
    }
}
