namespace Main.Domain.Common
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
