namespace Logs.Infrastructure.Persistence
{
    public class LogsContext : ILogsContext
    {
        public IMongoCollection<Logs> Logs { get; }

        public LogsContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Logs = database.GetCollection<Logs>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            LogsContextSeed.SeedData(Logs);
        }
    }
}
