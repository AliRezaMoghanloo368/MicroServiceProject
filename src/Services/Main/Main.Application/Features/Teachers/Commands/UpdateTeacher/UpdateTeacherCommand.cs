using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
