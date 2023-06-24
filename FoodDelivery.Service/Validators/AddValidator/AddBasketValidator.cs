using FluentValidation;
using FoodDelivery.DAL.Entity;

namespace FoodDelivery.Service.Validators.AddValidator
{
    public class AddBasketValidator:AbstractValidator<Basket>
    {
        public AddBasketValidator()
        {
            RuleFor(b => b.UserId)
                .NotNull()
                .NotEmpty().WithMessage("user id is requered");
        }
    }
}
