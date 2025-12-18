using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;

namespace Main.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly MainContext _context;
        public StudentRepository(MainContext context) : base(context)
        {
            _context = context;
        }
    }
}
