using FluentValidation;
using RestaurantManager.Api.Inputs.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class CreateRestaurantValidator : AbstractValidator<RestaurantInput>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("test adress");

            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage("test222");


            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");//
        }
    }
}
