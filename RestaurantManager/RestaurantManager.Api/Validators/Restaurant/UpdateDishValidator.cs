using FluentValidation;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Dishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class UpdateDishValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishValidator()
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
