using FluentValidation;
using FoodDelivery.Models.ViewModel.Account;

namespace FoodDelivery.Service.Validators.AuthValidator
{
    public class RegisterValidator:AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.Login)
                .NotEmpty()
                .NotNull().WithMessage("login is epmty")
                .MaximumLength(100).WithMessage("the maximum login length must be 100 characters")
                .MinimumLength(3).WithMessage("the minimum login length must be at least 3 characters");
            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull().WithMessage("password is epmty")
                .MinimumLength(6).WithMessage("the minimum password length must be at least 6 characters");
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .NotNull().WithMessage("confirm password is epmty");
        }
    }
}
