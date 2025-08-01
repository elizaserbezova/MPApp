using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories.Interfaces
{
    public interface IRecipeRepository
    {
        Recipe GetById(int id);
        IEnumerable<Recipe> GetAll();
        void Add(Recipe recipe);
        void Update(Recipe recipe);
        void Delete(Recipe recipe);
        void Save();
    }
}
