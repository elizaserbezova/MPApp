using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlannerApp.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IRecipeService _recipeService;

        public MealController(IMealService mealService, IRecipeService recipeService)
        {
            _mealService = mealService;
            _recipeService = recipeService;
        }

        public IActionResult Index()
        {
            var meals = _mealService.GetAll();
            return View(meals);
        }

        public IActionResult Details(int id)
        {
            var meal = _mealService.GetById(id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        public IActionResult Create()
        {
            var viewModel = new MealFormViewModel
            {
                Recipes = _recipeService.GetAll()
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
                viewModel.Recipes = _recipeService.GetAll()
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

            _mealService.Create(meal);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var meal = _mealService.GetById(id);
            if (meal == null) return NotFound();

            var viewModel = new MealFormViewModel
            {
                Id = meal.Id,
                Name = meal.Name,
                Date = meal.Date,
                TimeOfDay = meal.TimeOfDay,
                RecipeId = meal.RecipeId,
                Recipes = _recipeService.GetAll()
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
                viewModel.Recipes = _recipeService.GetAll()
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();
                return View(viewModel);
            }

            var meal = _mealService.GetById(viewModel.Id);
            if (meal == null) return NotFound();

            meal.Name = viewModel.Name;
            meal.Date = viewModel.Date;
            meal.TimeOfDay = viewModel.TimeOfDay;
            meal.RecipeId = viewModel.RecipeId;

            _mealService.Update(meal);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var meal = _mealService.GetById(id);
            if (meal == null) return NotFound();
            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _mealService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}