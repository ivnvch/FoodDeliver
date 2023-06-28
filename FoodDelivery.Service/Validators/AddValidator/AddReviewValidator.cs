using FluentValidation;
using FoodDelivery.Models.ViewModel.Review;

namespace FoodDelivery.Service.Validators.AddValidator
{
    public class AddReviewValidator:AbstractValidator<ReviewDto>
    {
        public AddReviewValidator()
        {
            RuleFor(r => r.CreationDate)
                .NotNull()
                .NotEmpty().WithMessage("data is empty");
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
