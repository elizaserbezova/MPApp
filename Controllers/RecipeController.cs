using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public IActionResult Index()
        {
            var recipes = _recipeService.GetAll();
            return View(recipes);
        }

        public IActionResult Details(int id)
        {
            var recipe = _recipeService.GetById(id);
            if (recipe == null)
                return NotFound();

            return View(recipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipeService.Create(recipe);
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        public IActionResult Edit(int id)
        {
            var recipe = _recipeService.GetById(id);
            if (recipe == null)
                return NotFound();

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipeService.Update(recipe);
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        public IActionResult Delete(int id)
        {
            var recipe = _recipeService.GetById(id);
            if (recipe == null)
                return NotFound();

            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _recipeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}