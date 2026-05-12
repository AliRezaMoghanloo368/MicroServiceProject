using Identity.Domain.Core.Common.SeedWork;
using System.Text.Json.Serialization;

namespace Identity.Domain.Core.AggregateModels.UserItems
{
    public class UserInfo : ValueObject<UserInfo>
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Email { get; private set; }

        [JsonConstructor]
        internal UserInfo(string fullName, string phoneNumber, string? email)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public static UserInfo Create(string fullName, string phoneNumber, string? email)
        {
            // می‌توانید اینجا لاجیک بیشتری اضافه کنید، مثلاً اعتبارسنجی
            return new UserInfo(fullName, phoneNumber, email);
        }

        protected override bool EqualsCore(UserInfo? valueObject)
        {
            return FullName == valueObject.FullName &&
                   PhoneNumber == valueObject.PhoneNumber &&
                   Email == valueObject.Email;
        }
    }
}
