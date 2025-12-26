using Logs.Domain.Models;

namespace Logs.Core.Contracts.Persistence
{
    public interface IHistoryRepository
    {
        Task<IEnumerable<History>> GetHistoriesAsync();
        Task<History> GetHistoryAsync(string id);
        Task<IEnumerable<History>> GetHistoryByUserNameAsync(string userName);
        Task CreateHistoryAsync(History history);
        Task<bool> UpdateHistoryAsync(History history);
        Task<bool> DeleteHistoryAsync(string id);
    }
}
