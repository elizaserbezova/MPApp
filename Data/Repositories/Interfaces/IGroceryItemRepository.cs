using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories.Interfaces
{
    public interface IGroceryItemRepository
    {
        GroceryItem GetById(int id);
        IEnumerable<GroceryItem> GetAll();
        void Add(GroceryItem item);
        void Update(GroceryItem item);
        void Delete(GroceryItem item);
        void Save();
    }
}
