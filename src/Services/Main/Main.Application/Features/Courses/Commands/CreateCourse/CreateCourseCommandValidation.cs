using FluentValidation;

namespace Main.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidation()
        {
            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("عنوان الزامی است")
                .NotNull()
                .MaximumLength(100).WithMessage("عنوان نباید بیشتر از 100 حرف باشد");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("توضیحات الزامی است")
                .NotNull()
                .MaximumLength(1000).WithMessage("توضیحات نباید بیشتر از 1000 حرف باشد");
        }
    }
}
