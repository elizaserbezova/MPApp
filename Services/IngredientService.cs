using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Ingredient> _repository;

        public IngredientService(IRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Ingredient?> GetIngredientByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            await _repository.AddAsync(ingredient);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            _repository.Update(ingredient);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await _repository.GetByIdAsync(id);
            if (ingredient != null)
            {
                _repository.Delete(ingredient);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
