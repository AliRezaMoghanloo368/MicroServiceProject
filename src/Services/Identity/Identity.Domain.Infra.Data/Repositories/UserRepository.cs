using Identity.Domain.Core.AggregateModels.Users;
using Identity.Domain.Core.Common.SeedWork.Interfaces;
using Identity.Domain.Core.Interfaces;
using Identity.Domain.Infra.Data.Context;
using Main.Infrastructure.Repositories;

namespace Identity.Domain.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IdentityContext _context;
        public UserRepository(IdentityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExistUserByUserNameAsync(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<User> GetUserForLoginAsync(string userName, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
