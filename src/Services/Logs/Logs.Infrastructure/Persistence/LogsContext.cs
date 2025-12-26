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
            var client = new MongoClient(configuration.GetSection("ConnectionStrings:Root").Value);
            var database = client.GetDatabase(configuration.GetSection("ConnectionStrings:DatabaseName").Value);
            Histories = database.GetCollection<History>(configuration.GetSection("ConnectionStrings:CollectionName").Value);
            HistoriesContextSeed.SeedData(Histories);
        }
    }
}
