using Logs.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Logs.Infrastructure.Persistence
{
    public class LogsContext : ILogsContext
    {
        public IMongoCollection<History> Histories { get; }

        public LogsContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
            var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);
            Histories = database.GetCollection<History>(configuration.GetSection("DatabaseSettings:CollectionName").Value);
            //LogsContextSeed.SeedData(Histories);
        }
    }
}
