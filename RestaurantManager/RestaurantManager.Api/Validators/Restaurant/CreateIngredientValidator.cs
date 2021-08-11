using FluentValidation;
using RestaurantManager.Api.Inputs.Restaurants;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class CreateIngredientValidator : AbstractValidator<IngredientInput>
    {
        public CreateIngredientValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be nonpositive");

            RuleFor(x => x.Name)
                     .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        }
    }
}
