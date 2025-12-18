using FluentValidation;

namespace Main.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandValidation : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidation()
        {
            RuleFor(x => x.Id)
                .LessThan(0).WithMessage("شناسه دانش آموز معتبر نیست.");

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
