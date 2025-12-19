using FluentValidation;
using Main.Application.Features.Teachers.Commands.CreateTeacher;

namespace Main.Application.Features.Students.Commands.CreateStudent
{
    public class CreateTeacherCommandValidation : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidation()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("نام الزامی است")
                .NotNull()
                .MaximumLength(100).WithMessage("نام نباید بیشتر از 100 حرف باشد");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است")
                .NotNull()
                .MaximumLength(100).WithMessage("نام خانوادگی نباید بیشتر از 100 حرف باشد");

            RuleFor(a => a.PhoneNumber)
                .NotEmpty().WithMessage("شماره تماس الزامی است")
                .NotNull()
                .MaximumLength(11).WithMessage("شماره تماس نباید بیشتر از 11 حرف باشد");
        }
    }
}
