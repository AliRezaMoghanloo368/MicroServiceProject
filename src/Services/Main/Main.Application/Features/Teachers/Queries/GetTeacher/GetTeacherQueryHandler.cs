using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Teachers;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Queries.GetTeacher
{
    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, Result<TeacherDto>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public GetTeacherQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<Result<TeacherDto>> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            var te = await _teacherRepository.GetByIdAsync(request.Id);
            var teacher = _mapper.Map<TeacherDto>(te);
            return Result<TeacherDto>.SuccessResult(teacher);
        }
    }
}
