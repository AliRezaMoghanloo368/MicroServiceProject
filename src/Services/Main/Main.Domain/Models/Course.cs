using Main.Domain.Common;
using System.Text.Json.Serialization;

namespace Main.Domain.Models
{
    public class Course: EntityBase
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public long TeacherId { get; set; }
        public string? CoverImageFileId { get; set; }

        [JsonIgnore]
        public Teacher Teacher { get; set; } = null!;
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
