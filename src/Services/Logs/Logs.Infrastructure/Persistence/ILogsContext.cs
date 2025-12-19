using MongoDB.Driver;
using Logs.Domain.Models;

namespace Logs.Infrastructure.Persistence
{
    public interface ILogsContext
    {
        IMongoCollection<History> Histories { get; }
    }
}
