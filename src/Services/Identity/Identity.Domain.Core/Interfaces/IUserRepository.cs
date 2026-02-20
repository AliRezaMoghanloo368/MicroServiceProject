using Identity.Domain.Core.AggregateModels.Users;
using Identity.Domain.Core.Common.SeedWork.Interfaces;

namespace Identity.Domain.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsExistUserByUserNameAsync(string userName);
        Task<User> GetUserByNameAsync(string userName);
        Task<User> GetUserForLoginAsync(string userName, string password);
    }
}
