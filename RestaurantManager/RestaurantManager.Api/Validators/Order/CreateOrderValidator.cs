using FluentValidation;
using RestaurantManager.Services.Commands.OrdersCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManager.Api.Validators.Order
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
              //.Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage("{PropertyName} - wrong format");
        }
    }
}
