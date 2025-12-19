namespace Logs.Infrastructure.Persistence
{
    public class LogsContextSeed
    {
        public static void SeedData(IMongoCollection<Logs> logsCollection)
        {
            bool existLogs = logsCollection.Find(p => true).Any();

            if (!existLogs)
            {
                logsCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Logs> GetSeedData()
        {
            return new List<Logs>()
            {
                new Logs()
                {

                },
                new Logs()
                {

                }
            };
        }
    }
}
