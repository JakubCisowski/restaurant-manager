using RestaurantManager.Services.Commands.Ingredients;
using RestaurantManager.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManager.Services.RestaurantServices.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task AddIngredientAsync(CreateIngredientCommand newIngredient);
        Task<bool> UpdateIngredientAsync(UpdateIngredientCommand ingredient);
        Task<IngredientDto> GetIngredientAsync(Guid id);
        Task<bool> DeleteIngredientAsync(Guid id);
    }
}
