using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;

namespace Main.Infrastructure.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(MainContext context) : base(context) { }

        public async Task InActiveAsync(long id)
        {
            var teacher = _context.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null)
                return;

            teacher.IsActive = false;
            await UpdateAsync(teacher);
        }
    }
}
