using Identity.Domain.Core.Common.SeedWork;

namespace Identity.Domain.Core.AggregateModels.UserItems
{
    public class UserId : StronglyTypeId<UserId>
    {
        public UserId(Guid value) : base(value)
        {
        }

        protected override bool EqualsCore(StronglyTypeId<UserId>? valueObject)
        {
            throw new NotImplementedException();
        }
    }
}
