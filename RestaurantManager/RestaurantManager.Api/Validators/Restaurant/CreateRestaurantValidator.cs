using FluentValidation;
using RestaurantManager.Api.Inputs.Restaurants;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class CreateRestaurantValidator : AbstractValidator<RestaurantInput>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
            //.Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage("{PropertyName} - wrong format");
        }
    }
}
