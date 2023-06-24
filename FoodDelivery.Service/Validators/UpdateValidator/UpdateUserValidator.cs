using FluentValidation;
using FoodDelivery.Models.ViewModel.User;

namespace FoodDelivery.Service.Validators.AddValidator
{
    public class UpdateUserValidator : AbstractValidator<UserViewModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(o => o.Id)
                .NotNull()
                .NotEmpty().WithMessage("id is requered");
            RuleFor(u => u.Login)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull().WithMessage("login is empty")
                .Must(IsValidLogin).WithMessage("login contains invalid characters");
            RuleFor(u => u.Token)
                .NotEmpty()
                .NotNull().WithMessage("token is empty");
            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull().WithMessage("password is epmty")
                .MinimumLength(6).WithMessage("the minimum password length must be at least 6 characters");
            RuleFor(u => u.Role)
                .NotEmpty()
                .NotNull().WithMessage("role is empty");
        }
        public bool IsValidLogin(string login)
        {
            login = login.Replace(" ", "");
            login = login.Replace("_", "");
            return login.All(char.IsLetter);
        }
    }
}
