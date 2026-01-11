using FluentValidation;
using WerehouseAPI.Dtos;

namespace WerehouseAPI.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Sender Id is required");

            RuleFor(x => x.Height)
                .NotEmpty().WithMessage("Height is required")
                .LessThan(3).WithMessage("Package can't be higher than 3m")
                .GreaterThan(0).WithMessage("Package can't be smaller than 0m");
            
            RuleFor(x => x.Weight)
                .NotEmpty().WithMessage("Weigth is required");

            RuleFor(x => x.Receiver)
                .NotEmpty().WithMessage("Receiver is required");
        }
    }
}
