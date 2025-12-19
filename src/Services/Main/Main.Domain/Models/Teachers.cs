using Main.Domain.Common;

namespace Main.Domain.Models
{
    public class Teacher : EntityBase
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ProfileImageFileId { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
