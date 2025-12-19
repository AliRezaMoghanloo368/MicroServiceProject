using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;

namespace Main.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(MainContext context) : base(context) { }

        public async Task InActiveAsync(long id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
                return;

            student.IsActive = false;
            await UpdateAsync(student);
        }
    }
}
