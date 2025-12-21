using Logs.Domain.Models;

namespace Logs.Core.Contracts.Persistence
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> GetHistoryAsync();
        Task<History> GetHistoryAsync(string id);
        Task<IEnumerable<History>> GetHistoryByUserNameAsync(string userName);
        //Task<IEnumerable<History>> GetHistoryByCategoryAsync(string category);
        Task CreateHistoryAsync(History history);
        Task<bool> UpdateHistoryAsync(History history);
        Task<bool> DeleteHistoryAsync(string id);
    }
}
