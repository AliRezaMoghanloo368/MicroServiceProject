using Main.Application.Dtos.Common;

namespace Main.Application.Dtos.Teachers
{
    public class TeacherDto : EntityBaseDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
