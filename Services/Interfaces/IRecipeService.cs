using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces
{
    public interface IRecipeService
    {
        Recipe GetById(int id);
        IEnumerable<Recipe> GetAll();
        void Create(Recipe recipe);
        void Update(Recipe recipe);
        void Delete(int id);
    }
}
