using Main.Domain.Models;
using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<Result<Student>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
    }
}
