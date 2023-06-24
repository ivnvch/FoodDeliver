using FluentValidation;
using FoodDelivery.DAL.Entity;

namespace FoodDelivery.Service.Validators.AddValidator
{
    public class AddOrderValidator : AbstractValidator<Order>
    {
        public AddOrderValidator()
        {
            RuleFor(o => o.DateCreate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("data is empty")
                .Must(IsValidTime).WithMessage("invalid date");
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
        protected bool IsValidTime(DateTime date)
        {
            DateTime currentDate = DateTime.Now;
            if (date == currentDate)
            {
                return true;
            }
            return false;
        }
    }
}
