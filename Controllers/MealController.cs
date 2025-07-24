using MealPlannerApp.Data;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Controllers
{
    public class MealController : Controller
    {
        private readonly ApplicationDbContext context;

        public MealController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var meals = context.Meals.Include(m => m.Recipe).ToList();
            return View(meals);
        }

        public IActionResult Details(int id)
        {
            var meal = context.Meals.Include(m => m.Recipe).FirstOrDefault(m => m.Id == id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        public IActionResult Create()
        {
            var viewModel = new MealFormViewModel
            {
                Recipes = context.Recipes
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MealFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Recipes = context.Recipes
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();
                return View(viewModel);
            }

            var meal = new Meal
            {
                Name = viewModel.Name,
                Date = viewModel.Date,
                TimeOfDay = viewModel.TimeOfDay,
                RecipeId = viewModel.RecipeId
            };

            context.Meals.Add(meal);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var meal = context.Meals.FirstOrDefault(m => m.Id == id);
            if (meal == null) return NotFound();

            var viewModel = new MealFormViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                Date = meal.Date,
                TimeOfDay = meal.TimeOfDay,
                RecipeId = meal.RecipeId,
                Recipes = context.Recipes
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MealFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Recipes = context.Recipes
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();
                return View(viewModel);
            }

            var meal = context.Meals.FirstOrDefault(m => m.Id == viewModel.Id);
            if (meal == null) return NotFound();

            meal.Name = viewModel.Name;
            meal.Date = viewModel.Date;
            meal.TimeOfDay = viewModel.TimeOfDay;
            meal.RecipeId = viewModel.RecipeId;

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var meal = context.Meals.Include(m => m.Recipe).FirstOrDefault(m => m.Id == id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var meal = context.Meals.FirstOrDefault(m => m.Id == id);
            if (meal != null)
            {
                context.Meals.Remove(meal);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}