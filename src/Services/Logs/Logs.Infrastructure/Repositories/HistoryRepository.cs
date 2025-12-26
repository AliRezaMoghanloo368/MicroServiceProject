using Logs.Core.Contracts.Persistence;
using Logs.Domain.Models;
using Logs.Infrastructure.Persistence;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Logs.Infrastructure.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        #region constructor
        private readonly ILogsContext _context;
        public HistoryRepository(ILogsContext logsContext)
        {
            _context = logsContext;
        }
        #endregion

        #region history repo
        public async Task<IEnumerable<History>> GetHistoriesAsync()
        {
            return await _context.Histories.Find(p => true).ToListAsync();
        }

        public async Task<History> GetHistoryAsync(string id)
        {
            return await _context.Histories.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<History>> GetHistoryByUserNameAsync(string userName)
        {
            FilterDefinition<History> filter =
                Builders<History>.Filter.Eq(p => p.UserName, userName);

            return await _context.Histories.Find(filter).ToListAsync();
        }

        public async Task CreateHistoryAsync(History logs)
        {
            await _context.Histories.InsertOneAsync(logs);
        }

        public async Task<bool> UpdateHistoryAsync(History logs)
        {
            var updateResult = await _context.Histories
                .ReplaceOneAsync(filter: p => p.Id == logs.Id, replacement: logs);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteHistoryAsync(string id)
        {
            FilterDefinition<History> filter =
                Builders<History>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Histories
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<History>?> GetHistoriesAsync(string userName,string? section,string? recordId)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return new List<History>();

            var query = _context.Histories
                .AsQueryable()
                .Where(x => x.UserName == userName);

            if (!string.IsNullOrWhiteSpace(section))
                query = query.Where(x => x.Section == section);

            if (!string.IsNullOrWhiteSpace(recordId))
                query = query.Where(x => x.RecordId == recordId);

            return await query.ToListAsync();
        }
        #endregion
    }
}
