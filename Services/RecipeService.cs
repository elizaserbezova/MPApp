using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Data.Repositories.Interfaces;

namespace MealPlannerApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public Recipe GetById(int id) => _repository.GetById(id);

        public IEnumerable<Recipe> GetAll() => _repository.GetAll();

        public void Create(Recipe recipe)
        {
            _repository.Add(recipe);
            _repository.Save();
        }

        public void Update(Recipe recipe)
        {
            _repository.Update(recipe);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var recipe = _repository.GetById(id);
            if (recipe != null)
            {
                _repository.Delete(recipe);
                _repository.Save();
            }
        }
    }
}
