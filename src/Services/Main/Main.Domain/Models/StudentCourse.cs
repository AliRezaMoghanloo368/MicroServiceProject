using Main.Domain.Common;

namespace Main.Domain.Models
{
    public class StudentCourse : EntityBase
    {
        public long StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public long CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
