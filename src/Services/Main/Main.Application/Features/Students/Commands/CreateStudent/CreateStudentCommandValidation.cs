using FluentValidation;

namespace Main.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandValidation:AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidation()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("نام الزامی است")
                .NotNull()
                .MaximumLength(100).WithMessage("نام نباید بیشتر از 100 حرف باشد");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است")
                .NotNull()
                .MaximumLength(100).WithMessage("نام خانوادگی نباید بیشتر از 100 حرف باشد");

            RuleFor(a => a.NationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است")
                .NotNull()
                .MaximumLength(10).WithMessage("کد ملی نباید بیشتر از 10 حرف باشد");
        }
    }
}
