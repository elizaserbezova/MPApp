using MealPlannerApp.Models;

namespace MealPlannerApp.Interfaces
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal?> GetMealByIdAsync(int id);
        Task AddMealAsync(Meal meal);
        Task UpdateMealAsync(Meal meal);
        Task DeleteMealAsync(int id);
    }
}
