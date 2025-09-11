using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _repository;

        public RecipeService(IRepository<Recipe> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _repository.AddAsync(recipe);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _repository.Update(recipe);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe != null)
            {
                _repository.Delete(recipe);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
