using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories.Interfaces
{
    public interface IMealRepository
    {
        Meal GetById(int id);
        IEnumerable<Meal> GetAll();
        void Add(Meal meal);
        void Update(Meal meal);
        void Delete(Meal meal);
        void Save();
    }
}
