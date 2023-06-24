using FluentValidation;
using FoodDelivery.Models.ViewModel.Account;

namespace FoodDelivery.Service.Validators.AuthValidator
{
    public class AddLoginValidator : AbstractValidator<LoginViewModel>
    {
        public AddLoginValidator()
        {
            RuleFor(l => l.Login)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull().WithMessage("login is empty")
                .Must(IsValidLogin).WithMessage("login contains invalid characters");
            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull().WithMessage("password is epmty")
                .MinimumLength(6).WithMessage("the minimum password length must be at least 6 characters");
        }
        public bool IsValidLogin(string login)
        {
            login = login.Replace(" ", "");
            login = login.Replace("_", "");
            return login.All(char.IsLetter);
        }
    }
}
