using MealPlannerApp.Data;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlannerApp.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly ApplicationDbContext _context;

        public IngredientController(IIngredientService ingredientService, ApplicationDbContext context)
        {
            _ingredientService = ingredientService;
            _context = context; // само за да зареждаме Recipes
        }

        public IActionResult Index()
        {
            var ingredients = _ingredientService.GetAll()
                .Select(i =>
                {
                    i.Recipe = _context.Recipes.FirstOrDefault(r => r.Id == i.RecipeId);
                    return i;
                }).ToList();

            return View(ingredients);
        }

        public IActionResult Details(int id)
        {
            var ingredient = _ingredientService.GetById(id);
            if (ingredient == null) return NotFound();

            ingredient.Recipe = _context.Recipes.FirstOrDefault(r => r.Id == ingredient.RecipeId);

            return View(ingredient);
        }

        public IActionResult Create()
        {
            var viewModel = new IngredientFormViewModel
            {
                Recipes = _context.Recipes
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
        public IActionResult Create(IngredientFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var ingredient = new Ingredient
                {
                    Name = viewModel.Name,
                    Quantity = viewModel.Quantity,
                    RecipeId = viewModel.RecipeId
                };

                _ingredientService.Create(ingredient);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Recipes = _context.Recipes
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var ingredient = _ingredientService.GetById(id);
            if (ingredient == null) return NotFound();

            var viewModel = new IngredientFormViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                RecipeId = ingredient.RecipeId,
                Recipes = _context.Recipes
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
        public IActionResult Edit(IngredientFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Recipes = _context.Recipes
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                    .ToList();
                return View(model);
            }

            var ingredient = _ingredientService.GetById(model.Id);
            if (ingredient == null) return NotFound();

            ingredient.Name = model.Name;
            ingredient.Quantity = model.Quantity;
            ingredient.RecipeId = model.RecipeId;

            _ingredientService.Update(ingredient);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var ingredient = _ingredientService.GetById(id);
            if (ingredient == null) return NotFound();

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ingredientService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}