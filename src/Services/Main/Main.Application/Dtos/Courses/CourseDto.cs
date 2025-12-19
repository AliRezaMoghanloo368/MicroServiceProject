using Main.Application.Dtos.StudentCourses;
using Main.Application.Dtos.Teachers;

namespace Main.Application.Dtos.Courses
{
    public class CourseDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public TeacherDto Teacher { get; set; } = null!;

        public List<StudentCourseDto> StudentCourses { get; set; } = new();

        public DateTime CreatedAt { get; set; }
    }
}
