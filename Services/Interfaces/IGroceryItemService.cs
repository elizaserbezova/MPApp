using MealPlannerApp.Models;

namespace MealPlannerApp.Services.Interfaces
{
    public interface IGroceryItemService
    {
        GroceryItem GetById(int id);
        IEnumerable<GroceryItem> GetAll();
        void Create(GroceryItem item);
        void Update(GroceryItem item);
        void Delete(int id);
    }
}
