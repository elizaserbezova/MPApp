using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        Ingredient GetById(int id);
        IEnumerable<Ingredient> GetAll();
        void Add(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(Ingredient ingredient);
        void Save();
    }
}
