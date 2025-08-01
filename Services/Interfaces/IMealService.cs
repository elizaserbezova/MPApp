using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces
{
    public interface IMealService
    {
        Meal GetById(int id);
        IEnumerable<Meal> GetAll();
        void Create(Meal meal);
        void Update(Meal meal);
        void Delete(int id);
    }
}
