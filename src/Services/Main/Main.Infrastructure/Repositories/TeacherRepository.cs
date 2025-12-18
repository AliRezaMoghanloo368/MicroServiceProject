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
    }
}
