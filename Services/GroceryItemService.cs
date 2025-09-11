using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Services
{
    public class GroceryItemService : IGroceryItemService
    {
        private readonly IRepository<GroceryItem> _repository;

        public GroceryItemService(IRepository<GroceryItem> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GroceryItem>> GetAllGroceryItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<GroceryItem?> GetGroceryItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddGroceryItemAsync(GroceryItem item)
        {
            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateGroceryItemAsync(GroceryItem item)
        {
            _repository.Update(item);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteGroceryItemAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item != null)
            {
                _repository.Delete(item);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
