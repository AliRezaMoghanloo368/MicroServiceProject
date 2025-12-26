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
                    Action = "new",
                    Description = "یک رکورد جدید ثبت شد."
                },
                new History()
                {
                    UserId = "1",
                    UserName = "test",
                    HostName = "DESKTOP-MOGHANLOO",
                    Action = "edit",
                    Description = "یک رکورد ویرایش شد."
                }
            };
        }
    }
}
