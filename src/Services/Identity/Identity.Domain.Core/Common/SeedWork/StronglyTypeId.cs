namespace Identity.Domain.Core.Common.SeedWork
{
    public abstract class StronglyTypeId<T> : ValueObject<StronglyTypeId<T>>
    {
        public Guid Value { get; }
        protected StronglyTypeId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException("Id can not be empty!");

            Value = value;
        }
    }
}

