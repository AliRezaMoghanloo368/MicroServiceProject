using Main.Application.Contracts.Persistence;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;
using Main.Domain.Models;

namespace Main.Application.Features.StudentCourses.Commands.CreateStudentCourse
{
    public class CreateStudentCourseCommandHandler : IRequestHandler<CreateStudentCourseCommand, Result<bool>>
    {
        private readonly IStudentCourseRepository _repo;
        public CreateStudentCourseCommandHandler(IStudentCourseRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<bool>> Handle(CreateStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repo.ExistsAsync(request.StudentId, request.CourseId);
            if (exists)
                return Result<bool>.ErrorResult("این دانش‌آموز قبلاً به این درس اضافه شده است.");

            var entity = new StudentCourse
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };
            
            await _repo.AddAsync(entity);
            return Result<bool>.SuccessResult(true);
        }
    }
}
