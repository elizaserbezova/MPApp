using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Data;
using MealPlannerApp.Models;
using Microsoft.EntityFrameworkCore;

public class MealPlanRepository : IMealPlanRepository
{
    private readonly ApplicationDbContext _context;

    public MealPlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public MealPlan GetById(int id) =>
        _context.MealPlans.Include(mp => mp.Meals).FirstOrDefault(mp => mp.Id == id);

    public IEnumerable<MealPlan> GetAll() =>
        _context.MealPlans.Include(mp => mp.Meals).ToList();

    public void Add(MealPlan mealPlan) => _context.MealPlans.Add(mealPlan);
    public void Update(MealPlan mealPlan) => _context.MealPlans.Update(mealPlan);
    public void Delete(MealPlan mealPlan) => _context.MealPlans.Remove(mealPlan);
    public void Save() => _context.SaveChanges();
}
