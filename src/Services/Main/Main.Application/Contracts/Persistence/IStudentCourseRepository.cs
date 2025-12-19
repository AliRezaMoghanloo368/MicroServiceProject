using Main.Domain.Models;

namespace Main.Application.Contracts.Persistence
{
    public interface IStudentCourseRepository : IGenericRepository<StudentCourse>
    {
        Task<bool> ExistsAsync(int studentId, int courseId);
    }
}
