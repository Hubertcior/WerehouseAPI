using FluentValidation;
using WerehouseAPI.Dtos;

namespace WerehouseAPI.Validators
{
    public class UpdatePackageStatusDtoValidator : AbstractValidator<UpdatePackageStatusDto>
    {
        public UpdatePackageStatusDtoValidator()
        {
            RuleFor(x => x.NewStatusId)
                .NotEmpty().WithMessage("New status can't be empty");
        }
    }
}
