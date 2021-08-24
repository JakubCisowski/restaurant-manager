using FluentValidation;
using RestaurantManager.Api.Inputs.Restaurants;
using RestaurantManager.Services.Commands.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantValidator()
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
