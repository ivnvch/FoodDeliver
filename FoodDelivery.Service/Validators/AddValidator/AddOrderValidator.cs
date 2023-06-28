using FluentValidation;
using FoodDelivery.Models.ViewModel.Order;

namespace FoodDelivery.Service.Validators.AddValidator
{
    public class UpdateOrderValidator : AbstractValidator<OrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(o => o.DateCreate)
                .NotNull()
                .NotEmpty().WithMessage("data is empty");
            RuleFor(o => o.DishId)
                .NotNull()
                .NotEmpty().WithMessage("dish id is requered");
            RuleFor(o => o.BasketId)
                .NotNull()
                .NotEmpty().WithMessage("basket id is requered");
            RuleFor(o => o.Address)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("address is empty")
                .Length(10, 100).WithMessage("length of address invalid");
            RuleFor(o => o.Commentary)
                .MaximumLength(200).WithMessage("maximum commentary length must be up to 200");
        }
    }
}
