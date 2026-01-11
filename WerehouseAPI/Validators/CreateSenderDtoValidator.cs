using FluentValidation;
using WerehouseAPI.Dtos;

namespace WerehouseAPI.Validators
{
    public class CreateSenderDtoValidator : AbstractValidator<CreateSenderDto>
    {
        public CreateSenderDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid format.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .Matches(@"^\d{2}-\d{3}$").WithMessage("Postal code must be in XX-XXX format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(9).WithMessage("Phone number is too short.");
        }
    }
}
