using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;

namespace Main.Infrastructure.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly MainContext _context;
        public TeacherRepository(MainContext context) : base(context)
        {
            _context = context;
        }

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
