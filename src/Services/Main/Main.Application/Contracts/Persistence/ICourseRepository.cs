using Main.Domain.Models;

namespace Main.Application.Contracts.Persistence
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task LoadTeacherAsync(Course course);
        Task LoadStudentCoursesAsync(Course course);
        Task<List<Course>> GetAllWithTeacherAsync();
    }
}
