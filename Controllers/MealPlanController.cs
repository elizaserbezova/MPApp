using MealPlannerApp.Data;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Controllers
{
    public class MealPlanController : Controller
    {
        private readonly ApplicationDbContext context;

        public MealPlanController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var plans = context.MealPlans.Include(p => p.Meals).ToList();
            return View(plans);
        }

        public IActionResult Details(int id)
        {
            var plan = context.MealPlans
                .Include(p => p.Meals)
                .ThenInclude(m => m.Recipe)
                .FirstOrDefault(p => p.Id == id);

            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        public IActionResult Create()
        {
            var meals = context.Meals.ToList();

            var viewModel = new MealPlanFormViewModel
            {
                AllMeals = meals.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MealPlanFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllMeals = context.Meals.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();

                return View(viewModel);
            }

            // Step 1: Create and save MealPlan
            var mealPlan = new MealPlan
            {
                Title = viewModel.Title,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };

            context.MealPlans.Add(mealPlan);
            context.SaveChanges(); // now mealPlan.Id is set

            // Load all meals into memory, then filter
            var allMeals = context.Meals.ToList(); // Fetches everything from DB
            var selectedMeals = allMeals
                .Where(m => viewModel.SelectedMealIds.Contains(m.Id))
                .ToList(); // Filter in C#

            foreach (var meal in selectedMeals)
            {
                meal.MealPlanId = mealPlan.Id;
            }

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var plan = context.MealPlans
                .Include(mp => mp.Meals)
                .FirstOrDefault(mp => mp.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            var viewModel = new MealPlanFormViewModel
            {
                Id = plan.Id,
                Title = plan.Title,
                StartDate = plan.StartDate,
                EndDate = plan.EndDate,
                SelectedMealIds = plan.Meals.Select(m => m.Id).ToList(),
                AllMeals = context.Meals.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MealPlanFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllMeals = context.Meals.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList();
                return View(viewModel);
            }

            var mealPlan = context.MealPlans
                .Include(mp => mp.Meals)
                .FirstOrDefault(mp => mp.Id == viewModel.Id);

            if (mealPlan == null)
            {
                return NotFound();
            }

            mealPlan.Title = viewModel.Title;
            mealPlan.StartDate = viewModel.StartDate;
            mealPlan.EndDate = viewModel.EndDate;

            // Clear and update selected meals
            mealPlan.Meals.Clear();
            var selectedMeals = context.Meals
                .Where(m => viewModel.SelectedMealIds.Contains(m.Id))
                .ToList();
            mealPlan.Meals.AddRange(selectedMeals);

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var plan = context.MealPlans.Find(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var plan = context.MealPlans
                .Include(mp => mp.Meals)
                .FirstOrDefault(p => p.Id == id);

            if (plan != null)
            {
                plan.Meals.Clear(); // Ensure FK deletion doesn't fail
                context.MealPlans.Remove(plan);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}