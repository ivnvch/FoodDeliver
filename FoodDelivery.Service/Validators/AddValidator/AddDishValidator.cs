using FluentValidation;
using FoodDelivery.Models.Entity.DTO;

namespace FoodDelivery.Service.Validators.AddValidator
{
    public class AddDishValidator:AbstractValidator<DishCreateDTO>
    {
        public AddDishValidator()
        {
            RuleFor(d => d.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty()  .NotNull().WithMessage("name is empty")
                .Length(2, 20).WithMessage("length of name invalid")
                .Must(IsValidName).WithMessage("type name invalid character");
            RuleFor(d => d.Description)
                .MaximumLength(200).WithMessage("maximum description length must be up to 200");
            RuleFor(d => d.Price)
                .NotEmpty()
                .NotNull().WithMessage("price is empty");
            RuleFor(d => d.Weight)
                .NotEmpty()
                .NotNull().WithMessage("weight is empty");
            RuleFor(d => d.Amount)
               .NotEmpty()
               .NotNull().WithMessage("amount is empty");
        }
        public bool IsValidName(string name)
        {
            name = name.Replace(" ", "");
            return name.All(char.IsLetter);
        }
    }
}
