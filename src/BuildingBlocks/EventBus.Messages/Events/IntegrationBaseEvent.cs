namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid().ToString();
            CreateAt = DateTime.UtcNow;
        }
        public IntegrationBaseEvent(string? id, DateTime createAt)
        {
            Id = id;
            CreateAt = createAt;
        }
        public string? Id { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
