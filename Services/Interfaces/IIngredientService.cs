using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces
{
    public interface IIngredientService
    {
        Ingredient GetById(int id);
        IEnumerable<Ingredient> GetAll();
        void Create(Ingredient ingredient);
        void Update(Ingredient ingredient);
        void Delete(int id);
    }
}
