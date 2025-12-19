using Logs.Domain.Models;
using MongoDB.Driver;

namespace Logs.Infrastructure.Persistence
{
    public class HistoriesContextSeed
    {
        public static void SeedData(IMongoCollection<History> historiesCollection)
        {
            bool existHistories = historiesCollection.Find(p => true).Any();

            if (!existHistories)
            {
                historiesCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<History> GetSeedData()
        {
            return new List<History>()
            {
                new History()
                {

                },
                new History()
                {

                }
            };
        }
    }
}
