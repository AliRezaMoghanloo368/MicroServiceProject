using MediatR;
using SharedLibrary.Patterns.ResultPattern;

namespace Main.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Result<bool>>
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        //public string? ProfileImageFileId { get; set; }
    }
}
