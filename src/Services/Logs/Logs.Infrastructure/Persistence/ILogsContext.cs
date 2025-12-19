namespace Logs.Infrastructure.Persistence
{
    public interface ILogsContext
    {
        IMongoCollection<Logs> Logs { get; }
    }
}
