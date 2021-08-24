﻿using FluentValidation;
using RestaurantManager.Services.Commands.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Validators.Restaurant
{
    public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be nonpositive");

            RuleFor(x => x.Name)
                     .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        }
    }
}
