using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Services
{
    public class MealService : IMealService
    {
        private readonly IRepository<Meal> _mealRepository;

        public MealService(IRepository<Meal> mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await _mealRepository.GetAllAsync();
        }

        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            return await _mealRepository.GetByIdAsync(id);
        }

        public async Task AddMealAsync(Meal meal)
        {
            await _mealRepository.AddAsync(meal);
            await _mealRepository.SaveChangesAsync();
        }

        public async Task UpdateMealAsync(Meal meal)
        {
            _mealRepository.Update(meal);
            await _mealRepository.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            if (meal != null)
            {
                _mealRepository.Delete(meal);
                await _mealRepository.SaveChangesAsync();
            }
        }
    }
}
