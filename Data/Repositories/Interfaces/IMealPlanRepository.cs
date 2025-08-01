using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories.Interfaces
{
    public interface IMealPlanRepository
    {
        MealPlan GetById(int id);
        IEnumerable<MealPlan> GetAll();
        void Add(MealPlan mealPlan);
        void Update(MealPlan mealPlan);
        void Delete(MealPlan mealPlan);
        void Save();
    }
}
