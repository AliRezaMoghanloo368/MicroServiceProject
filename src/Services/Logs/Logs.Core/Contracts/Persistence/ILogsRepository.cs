using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.Core.Contracts.Persistence
{
    public interface ILogsRepository
    {
        Task<IEnumerable<Logs>> GetLogs();
        Task<Logs> GetLog(string id);
        Task<IEnumerable<Logs>> GetLogsByName(string name);
        Task<IEnumerable<Logs>> GetLogsByCategory(string category);
        Task CreateLogs(Logs logs);
        Task<bool> UpdateLogs(Logs logs);
        Task<bool> DeleteLogs(string id);
    }
}
