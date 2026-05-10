using Identity.Domain.Core.AggregateModels.UserItems;

namespace Identity.Domain.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity?> GetByUserNameAsync(string name);
        Task<bool> IsExistUserByUserNameAsync(string name);
        Task<UserEntity> CreateAsync(UserEntity entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(UserEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
