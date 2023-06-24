using FluentValidation;
using FoodDelivery.DAL.Entity;

namespace FoodDelivery.Service.Validators.UpdateValidator
{
    public class UpdateVendorValidator : AbstractValidator<Vendor>
    {
        public UpdateVendorValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .NotEmpty().WithMessage("id is requered");
            RuleFor(v => v.OpeningTime)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .NotEmpty().WithMessage("data is empty")
               .Must(IsValidTime).WithMessage("invalid date");
            RuleFor(v => v.ClosingTime)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .NotEmpty().WithMessage("data is empty")
               .Must(IsValidTime).WithMessage("invalid date");
            RuleFor(v => v.TimeOfDelivery)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .NotEmpty().WithMessage("data is empty")
               .Must(IsValidTime).WithMessage("invalid date");
            RuleFor(v => v.Type)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("type is empty")
                .Length(3, 15).WithMessage("length of type invalid")
                .Must(IsValidName).WithMessage("type contains invalid characters");
            RuleFor(v => v.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .NotNull().WithMessage("name is empty")
                .Length(2, 20).WithMessage("length of name invalid");
            RuleFor(v => v.Description)
                .MaximumLength(200).WithMessage("maximum description length must be up to 200");
            RuleFor(v => v.Address)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotNull()
               .NotEmpty().WithMessage("address is empty")
               .Length(10, 100).WithMessage("length of name invalid");
            RuleFor(v => v.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty().WithMessage("phone number is empty")
                .MaximumLength(13).WithMessage("phone number length should not be more than 13");
        }
        public bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            return name.All(char.IsLetter);
        }
        protected bool IsValidTime(DateTime date)
        {
            if (date.ToString() != date.ToShortTimeString())
            {
                return false;
            }
            return true;
        }
    }
}
