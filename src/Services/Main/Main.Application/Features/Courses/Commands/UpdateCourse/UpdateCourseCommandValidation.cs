using FluentValidation;
using Main.Application.Contracts.Persistence;
using Main.Application.Features.Courses.Commands.UpdateCourse;

namespace Main.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidation : AbstractValidator<UpdateCourseCommand>
    {
        private readonly ICourseRepository _repo;
        public UpdateCourseCommandValidation(ICourseRepository repo)
        {
            _repo = repo;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه کتاب معتبر نیست");

            RuleFor(x => x.Id)
                .MustAsync(async (id, token) =>
                {
                    var result = await _repo.GetByIdAsync(id);
                    return result == null ? false : true;
                }).WithMessage("کتابی یافت نشد");

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
