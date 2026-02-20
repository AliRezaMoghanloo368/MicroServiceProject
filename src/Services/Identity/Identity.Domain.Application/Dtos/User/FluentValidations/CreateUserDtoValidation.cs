using FluentValidation;
using Identity.Domain.Core.Interfaces;

namespace Identity.Domain.Application.Dtos.User.FluentValidations
{
    public class CreateUserDtoValidation : AbstractValidator<CreateUserDto>
    {
        private readonly IUserRepository _repository;
        public CreateUserDtoValidation(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.UserInfo.FullName)
                .NotEmpty().WithMessage($"نام و نام خانوادگی نباید خالی باشد! لطفا درانتخاب آن دقت فرمایید.");

            RuleFor(x => x.UserName)
            .MustAsync(async (username, token) =>
            {
                var result = await _repository.IsExistUserByUserNameAsync(username);
                return !result;
            }).WithMessage("نام کاربری مورد نظر موجود می باشد.");

            RuleFor(x => x.UserInfo.PhoneNumber)
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد!");

            RuleFor(x => x.UserInfo.Email)
                .EmailAddress().WithMessage("ایمیل وارد شده صحیح نمی باشد.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور نباید خالی باشد!")
                .Matches(@"[A-Z]+").WithMessage("حداقل یک حرف بزرگ می خواهد.")
                .MinimumLength(6).WithMessage("طول پسورد شما کمتر از 6 کاراکتر می باشد.");
        }
    }
}
