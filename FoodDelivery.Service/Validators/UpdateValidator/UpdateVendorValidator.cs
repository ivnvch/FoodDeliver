using FluentValidation;
using FoodDelivery.DAL.Entity;

namespace FoodDelivery.Service.Validators.UpdateValidator
{
    public class UpdateVendorValidator : AbstractValidator<Vendor>
    {
        public UpdateVendorValidator()
        {
            RuleFor(v => v.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 20).WithMessage("length of name invalid")
                .Must(IsValidName).WithMessage("name contains invalid characters");

        }
        public bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("_", "");
            return name.All(char.IsLetter);
        }
    }
}
