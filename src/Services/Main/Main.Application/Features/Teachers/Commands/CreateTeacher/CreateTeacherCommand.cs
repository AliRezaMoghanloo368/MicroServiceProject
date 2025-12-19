using Main.Domain.Models;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<Result<Teacher>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
