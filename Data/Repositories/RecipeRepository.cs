using MealPlannerApp.Models;
using MealPlannerApp.Data.Repositories.Interfaces;

namespace MealPlannerApp.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Recipe GetById(int id) => _context.Recipes.FirstOrDefault(r => r.Id == id);

        public IEnumerable<Recipe> GetAll() => _context.Recipes.ToList();

        public void Add(Recipe recipe) => _context.Recipes.Add(recipe);

        public void Update(Recipe recipe) => _context.Recipes.Update(recipe);

        public void Delete(Recipe recipe) => _context.Recipes.Remove(recipe);

        public void Save() => _context.SaveChanges();
    }
}
