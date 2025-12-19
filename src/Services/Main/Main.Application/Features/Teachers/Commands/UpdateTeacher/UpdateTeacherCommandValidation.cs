using FluentValidation;
using Main.Application.Contracts.Persistence;

namespace Main.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandValidation : AbstractValidator<UpdateTeacherCommand>
    {
        private readonly ITeacherRepository _repo;
        public UpdateTeacherCommandValidation(ITeacherRepository repo)
        {
            _repo = repo;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه معلم معتبر نیست");

            RuleFor(x => x.Id)
                .MustAsync(async (id, token) =>
                {
                    var result = await _repo.GetByIdAsync(id);
                    return result == null ? false : true;
                }).WithMessage("معلمی یافت نشد");

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
