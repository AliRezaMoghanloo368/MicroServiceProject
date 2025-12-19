using AutoMapper;
using Main.Application.Contracts.Persistence;
using Main.Application.Dtos.Teachers;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Queries.GetTeachers
{
    public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, Result<List<TeacherDto>>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public GetTeachersQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<TeacherDto>>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var list = await _teacherRepository.GetAllAsync();
            var teacherList = _mapper.Map<List<TeacherDto>>(list);
            return Result<List<TeacherDto>>.SuccessResult(teacherList);
        }
    }
}
