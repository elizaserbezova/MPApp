using MealPlannerApp.Data;
using MealPlannerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext context;

        public RecipeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index ()
        {
            var recipes = context.Recipes.ToList();
            return View(recipes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                context.Recipes.Add(recipe);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public IActionResult Details(int Id)
        {
            var recipe = context.Recipes.FirstOrDefault(r => r.Id == Id);
            if(recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        public IActionResult Delete(int Id)
        {
            var recipe = context.Recipes.FirstOrDefault(r => r.Id == Id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var recipe = context.Recipes.FirstOrDefault(r => r.Id == Id);
            if (recipe != null)
            {
                context.Recipes.Remove(recipe);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var recipe = context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
                return NotFound();

            return View(recipe);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                context.Recipes.Update(recipe);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }
    }
}
