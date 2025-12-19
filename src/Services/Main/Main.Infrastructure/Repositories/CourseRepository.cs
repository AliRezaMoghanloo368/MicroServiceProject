using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly MainContext _context;
        public CourseRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public async Task LoadTeacherAsync(Course course)
        {
            await _context.Entry(course)
                .Reference(x => x.Teacher)
                .LoadAsync();
        }

        public async Task LoadStudentCoursesAsync(Course course)
        {
            await _context.Entry(course)
                .Collection(x => x.StudentCourses)
                .LoadAsync();
        }

        public async Task<List<Course>> GetAllWithTeacherAsync()
        {
            return await _context.Courses
                .Include(x => x.Teacher)
                .ToListAsync();
        }

        public async Task<List<Course>> GetAllWithTeacherAndStudentsAsync()
        {
            return await _context.Courses
                .Include(x => x.Teacher)
                .Include(x => x.StudentCourses)
                    .ThenInclude(x => x.Student)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdWithStudentsAsync(long id)
        {
            return await _context.Courses
                .Include(x => x.Teacher)
                .Include(x => x.StudentCourses)
                    .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
