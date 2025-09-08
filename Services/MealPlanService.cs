using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Services
{
    public class MealPlanService : IMealPlanService
    {
        private readonly IRepository<MealPlan> _mealPlanRepository;

        public MealPlanService(IRepository<MealPlan> mealPlanRepository)
        {
            _mealPlanRepository = mealPlanRepository;
        }

        public async Task<IEnumerable<MealPlan>> GetAllMealPlansAsync()
        {
            return await _mealPlanRepository.GetAllAsync();
        }

        public async Task<MealPlan?> GetMealPlanByIdAsync(int id)
        {
            return await _mealPlanRepository.GetByIdAsync(id);
        }

        public async Task AddMealPlanAsync(MealPlan mealPlan)
        {
            await _mealPlanRepository.AddAsync(mealPlan);
            await _mealPlanRepository.SaveChangesAsync();
        }

        public async Task UpdateMealPlanAsync(MealPlan mealPlan)
        {
            _mealPlanRepository.Update(mealPlan);
            await _mealPlanRepository.SaveChangesAsync();
        }

        public async Task DeleteMealPlanAsync(int id)
        {
            var mealPlan = await _mealPlanRepository.GetByIdAsync(id);
            if (mealPlan != null)
            {
                _mealPlanRepository.Delete(mealPlan);
                await _mealPlanRepository.SaveChangesAsync();
            }
        }
    }
}