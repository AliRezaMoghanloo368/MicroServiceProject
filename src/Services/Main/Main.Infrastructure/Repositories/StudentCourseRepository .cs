using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Main.Infrastructure.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(MainContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int studentId, int courseId)
        {
            return await _context.StudentCourses
                .AnyAsync(x => x.StudentId == studentId && x.CourseId == courseId);
        }
    }
}
