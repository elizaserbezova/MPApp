using MealPlannerApp.Data;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Controllers
{
    public class IngredientController : Controller
    {
        private readonly ApplicationDbContext context;

        public IngredientController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var ingredients = context.Ingredients.Include(i => i.Recipe).ToList();
            return View(ingredients);
        }

        public IActionResult Details(int id)
        {
            var ingredient = context.Ingredients.Include(i => i.Recipe).FirstOrDefault(i => i.Id == id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        public IActionResult Create()
        {
            var viewModel = new IngredientFormViewModel
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
        public IActionResult Create(IngredientFormViewModel viewModel)
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"❌ {state.Key}: {error.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                var ingredient = new Ingredient
                {
                    Name = viewModel.Name,
                    Quantity = viewModel.Quantity,
                    RecipeId = viewModel.RecipeId
                };

                context.Ingredients.Add(ingredient);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            viewModel.Recipes = context.Recipes
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var ingredient = context.Ingredients.FirstOrDefault(i => i.Id == id);
            if (ingredient == null) return NotFound();

            var viewModel = new IngredientFormViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                RecipeId = ingredient.RecipeId,
                Recipes = context.Recipes
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IngredientFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Recipes = context.Recipes
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                    .ToList();
                return View(model);
            }

            var ingredient = context.Ingredients.FirstOrDefault(i => i.Id == model.Id);
            if (ingredient == null) return NotFound();

            ingredient.Name = model.Name;
            ingredient.Quantity = model.Quantity;
            ingredient.RecipeId = model.RecipeId;

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var ingredient = context.Ingredients.FirstOrDefault(i => i.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ingredient = context.Ingredients.Find(id);
            if (ingredient == null)
                return NotFound();

            context.Ingredients.Remove(ingredient);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
