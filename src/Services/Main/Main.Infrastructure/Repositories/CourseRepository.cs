using Main.Application.Contracts.Persistence;
using Main.Domain.Models;
using Main.Infrastructure.Persistence;

namespace Main.Infrastructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly MainContext _context;
        public CourseRepository(MainContext context) : base(context)
        {
            _context = context;
        }
    }
}
