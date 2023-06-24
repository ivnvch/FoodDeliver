
using FluentValidation;
using FoodDelivery.DAL.Entity;

namespace FoodDelivery.Service.Validators.UpdateValidator
{
    public class UpdateReviewValidator : AbstractValidator<Review>
    {
        public UpdateReviewValidator()
        {
            RuleFor(v => v.Id)
               .NotNull()
               .NotEmpty().WithMessage("id is requered");
            RuleFor(r => r.CreationDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("data is empty")
                .Must(IsValidTime).WithMessage("invalid date");
            RuleFor(r => r.UserId)
                .NotNull()
                .NotEmpty().WithMessage("user id is requered");
            RuleFor(r => r.VendorId)
                .NotNull()
                .NotEmpty().WithMessage("vendor id is requered");
            RuleFor(r => r.CustomerRating)
                .NotNull()
                .NotEmpty().WithMessage("customer rating is empty")
                .Must(IsValidRating).WithMessage("score must be from 0 to 5");
            RuleFor(r => r.Description)
                .MaximumLength(200).WithMessage("maximum description length must be up to 200");
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
        protected bool IsValidRating(double rating)
        {
            if (rating >= 0 && rating <= 5)
            {
                return true;
            }
            return false;
        }
    }
}
