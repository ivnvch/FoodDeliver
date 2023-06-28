using FluentValidation;
using FoodDelivery.Models.ViewModel.Profile;

namespace FoodDelivery.Service.Validators.UpdateValidator
{
    public class UpdateProfileValidator : AbstractValidator<ProfileViewModel>
    {
        public UpdateProfileValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .NotEmpty().WithMessage("id is requered");
            RuleFor(p => p.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("first name is empty")
                .MaximumLength(25).WithMessage("maximum first name length 25")
                .Must(IsValidName).WithMessage("first name contains invalid characters");
            RuleFor(p => p.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("last name is empty")
                .MaximumLength(50).WithMessage("maximum last name length 25")
                .Must(IsValidName).WithMessage("last name contains invalid characters");
            RuleFor(p => p.MiddleName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("middle name is empty")
                .MaximumLength(50).WithMessage("maximum middle name length 25");
            RuleFor(p => p.DateCreated)
                .NotNull()
                .NotEmpty().WithMessage("data is empty");
            RuleFor(p => p.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("phone number is empty")
                .MaximumLength(13).WithMessage("phone number length should not be more than 13");
            RuleFor(l => l.Login)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull().WithMessage("login is empty")
                .Must(IsValidLogin).WithMessage("login contains invalid characters");
        }
        public bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            return name.All(char.IsLetter);
        }
        public bool IsValidLogin(string login)
        {
            login = login.Replace(" ", "");
            login = login.Replace("_", "");
            return login.All(char.IsLetter);
        }
    }
}
