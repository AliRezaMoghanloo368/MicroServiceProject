namespace EventBus.Messages.Events
{
    public class LogsHistoryEvent : IntegrationBaseEvent
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public string? Section { get; set; }
        public string? RecordId { get; set; }
        public DateTime CreateAt { get; set; }
        public string Action { get; set; } //new, edit, delete
        public string Description { get; set; }
    }
}
