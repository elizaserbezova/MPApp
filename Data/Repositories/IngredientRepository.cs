using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Models;

namespace MealPlannerApp.Data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ingredient GetById(int id) => _context.Ingredients.FirstOrDefault(i => i.Id == id);

        public IEnumerable<Ingredient> GetAll() => _context.Ingredients.ToList();

        public void Add(Ingredient ingredient) => _context.Ingredients.Add(ingredient);

        public void Update(Ingredient ingredient) => _context.Ingredients.Update(ingredient);

        public void Delete(Ingredient ingredient) => _context.Ingredients.Remove(ingredient);

        public void Save() => _context.SaveChanges();
    }
}
