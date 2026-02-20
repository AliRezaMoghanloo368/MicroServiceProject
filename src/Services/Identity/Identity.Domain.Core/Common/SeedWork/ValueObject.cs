namespace Identity.Domain.Core.Common.SeedWork
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            if (ReferenceEquals(obj, null))
                return false;

            return EqualsCore(valueObject);
        }

        public static bool operator ==(ValueObject<T> obj1, ValueObject<T> obj2)
        {
            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
                return false;
            if (ReferenceEquals(obj1, null) && ReferenceEquals(obj2, null))
                return true;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(ValueObject<T> obj1, ValueObject<T> obj2)
        => !(obj1 == obj2);

        protected abstract bool EqualsCore(T? valueObject);
    }
}
