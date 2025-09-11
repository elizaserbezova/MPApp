using MealPlannerApp.Models;

namespace MealPlannerApp.Interfaces
{
    public interface IGroceryItemService
    {
        Task<IEnumerable<GroceryItem>> GetAllGroceryItemsAsync();
        Task<GroceryItem?> GetGroceryItemByIdAsync(int id);
        Task AddGroceryItemAsync(GroceryItem item);
        Task UpdateGroceryItemAsync(GroceryItem item);
        Task DeleteGroceryItemAsync(int id);
    }
}
