namespace Logs.Domain.Models
{
    public class Logs
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public int Action { get; set; }
        public string Description { get; set; }
    }
}
