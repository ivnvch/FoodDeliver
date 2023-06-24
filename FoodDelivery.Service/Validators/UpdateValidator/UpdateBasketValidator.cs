using FluentValidation;
using FoodDelivery.DAL.Entity;

namespace FoodDelivery.Service.Validators.UpdateValidator
{
    public class UpdateBasketValidator:AbstractValidator<Basket>
    {
        public UpdateBasketValidator()
        {
            RuleFor(b => b.Id)
                .NotNull()
                .NotEmpty().WithMessage("id is requered");
            RuleFor(b => b.UserId)
                .NotNull()
                .NotEmpty().WithMessage("user id is requered");
        }
    }
}
