using Identity.Domain.Core.Common.SeedWork;

namespace Identity.Domain.Core.AggregateModels.UserItems
{
    public class UserInfo : ValueObject<UserInfo>
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        internal UserInfo(string fullName, string phoneNumber, string? email)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        private UserInfo() { }

        protected override bool EqualsCore(UserInfo? valueObject)
        {
            return FullName == valueObject.FullName &&
                   PhoneNumber == valueObject.PhoneNumber &&
                   Email == valueObject.Email;
        }
    }
}
