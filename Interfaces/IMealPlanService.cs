using MealPlannerApp.Models;

namespace MealPlannerApp.Interfaces
{
    public interface IMealPlanService
    {
        Task<IEnumerable<MealPlan>> GetAllMealPlansAsync();
        Task<MealPlan?> GetMealPlanByIdAsync(int id);
        Task AddMealPlanAsync(MealPlan mealPlan);
        Task UpdateMealPlanAsync(MealPlan mealPlan);
        Task DeleteMealPlanAsync(int id);
    }
}
