using Main.Domain.Common;

namespace Main.Domain.Models
{
    public class Course: EntityBase
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public string? CoverImageFileId { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
