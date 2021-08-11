using FluentValidation;
using RestaurantManager.Api.Inputs.Restaurants;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class CreateDishValidator : AbstractValidator<DishInput>
    {
        public CreateDishValidator()
        {
            RuleFor(x => x.BasePrice)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be nonpositive");

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        }
    }
}
