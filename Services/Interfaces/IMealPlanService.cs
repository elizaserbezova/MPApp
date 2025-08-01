using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces
{
    public interface IMealPlanService
    {
        MealPlan GetById(int id);
        IEnumerable<MealPlan> GetAll();
        void Create(MealPlan mealPlan);
        void Update(MealPlan mealPlan);
        void Delete(int id);
    }
}
