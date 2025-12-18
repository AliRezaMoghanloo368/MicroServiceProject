using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Students;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Queries.GetStudents
{
    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, Result<List<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var list = await _studentRepository.GetAllAsync();
            var studentList = _mapper.Map<List<StudentDto>>(list);
            return Result<List<StudentDto>>.SuccessResult(studentList);
        }
    }
}
