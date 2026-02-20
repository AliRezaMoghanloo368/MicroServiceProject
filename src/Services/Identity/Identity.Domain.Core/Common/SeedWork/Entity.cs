namespace Identity.Domain.Core.Common.SeedWork
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<T>;
            return entity != null && GetType() == entity.GetType() &&
                   EqualityComparer<T>.Default.Equals(Id, entity.Id);
        }

        public static bool operator ==(Entity<T> obj1, Entity<T> obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;
            if (ReferenceEquals(obj1, null))
                return false;
            if (ReferenceEquals(obj2, null))
                return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Entity<T> obj1, Entity<T> obj2)
        => !(obj1 == obj2);
    }
}
