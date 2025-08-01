using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Data.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext _context;

        public MealRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Meal GetById(int id) => _context.Meals.Include(m => m.Recipe).FirstOrDefault(m => m.Id == id);

        public IEnumerable<Meal> GetAll() => _context.Meals.Include(m => m.Recipe).ToList();

        public void Add(Meal meal) => _context.Meals.Add(meal);

        public void Update(Meal meal) => _context.Meals.Update(meal);

        public void Delete(Meal meal) => _context.Meals.Remove(meal);

        public void Save() => _context.SaveChanges();
    }
}
