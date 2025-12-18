using Main.Domain.Common;

namespace Main.Domain.Models
{
    public class Student : EntityBase
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        public string? ProfileImageFileId { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
