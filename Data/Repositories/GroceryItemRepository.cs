using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories
{
    public class GroceryItemRepository : IGroceryItemRepository
    {
        private readonly ApplicationDbContext _context;

        public GroceryItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public GroceryItem GetById(int id) => _context.GroceryItems.FirstOrDefault(g => g.Id == id);

        public IEnumerable<GroceryItem> GetAll() => _context.GroceryItems.ToList();

        public void Add(GroceryItem item) => _context.GroceryItems.Add(item);

        public void Update(GroceryItem item) => _context.GroceryItems.Update(item);

        public void Delete(GroceryItem item) => _context.GroceryItems.Remove(item);

        public void Save() => _context.SaveChanges();
    }
}
