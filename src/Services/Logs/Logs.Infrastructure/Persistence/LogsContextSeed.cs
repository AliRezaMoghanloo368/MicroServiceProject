using Logs.Domain.Models;
using MongoDB.Driver;

namespace Logs.Infrastructure.Persistence
{
    public class HistoriesContextSeed
    {
        public static void SeedData(IMongoCollection<History> histories)
        {
            bool isExist = histories.Find(p => true).Any();

            if (!isExist)
            {
                histories.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<History> GetSeedData()
        {
            return new List<History>()
            {
                new History()
                {
                    UserId = "1",
                    UserName = "test",
                    HostName = "DESKTOP-MOGHANLOO",
                    Section = "section1",
                    RecordId = "1",
                    Action = "seeddata",
                    Description = "رکورد مورد نظر تستی می باشد."
                },
                new History()
                {
                    UserId = "1",
                    UserName = "test",
                    HostName = "DESKTOP-MOGHANLOO",
                    Section = "section2",
                    RecordId = "2",
                    Action = "seeddata",
                    Description = "رکورد مورد نظر تستی می باشد."
                }
            };
        }
    }
}
