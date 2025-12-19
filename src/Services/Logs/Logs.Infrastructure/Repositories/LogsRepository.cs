using Logs.Core.Contracts.Persistence;
using Logs.Infrastructure.Persistence;

namespace Logs.Infrastructure.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        #region constructor
        private readonly ILogsContext _context;

        public LogsRepository(ILogsContext logsContext)
        {
            _context = logsContext;
        }
        #endregion

        #region logs repo
        public async Task<IEnumerable<Logs>> GetLogss()
        {
            return await _context.Logss.Find(p => true).ToListAsync();
        }

        public async Task<Logs> GetLogs(string id)
        {
            return await _context.Logss.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Logs>> GetLogsByName(string name)
        {
            FilterDefinition<Logs> filter =
                Builders<Logs>.Filter.Eq(p => p.Name, name);

            return await _context.Logss.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Logs>> GetLogsByCategory(string category)
        {
            FilterDefinition<Logs> filter =
                Builders<Logs>.Filter.Eq(p => p.Category, category);

            return await _context.Logss.Find(filter).ToListAsync();
        }

        public async Task CreateLogs(Logs logs)
        {
            await _context.Logss.InsertOneAsync(logs);
        }

        public async Task<bool> UpdateLogs(Logs logs)
        {
            var updateResult = await _context.Logss
                .ReplaceOneAsync(filter: p => p.Id == logs.Id, replacement: logs);


            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteLogs(string id)
        {
            FilterDefinition<Logs> filter =
                Builders<Logs>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Logss
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }
        #endregion
    }
}
