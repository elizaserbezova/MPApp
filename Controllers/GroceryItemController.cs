using MealPlannerApp.Data;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Controllers
{
    public class GroceryItemController : Controller
    {
        private readonly ApplicationDbContext context;

        public GroceryItemController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var items = context.GroceryItems.Include(g => g.MealPlan).ToList();
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var item = context.GroceryItems.Include(g => g.MealPlan).FirstOrDefault(g => g.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create()
        {
            var viewModel = new GroceryItemFormViewModel
            {
                MealPlans = context.MealPlans
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Title })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroceryItemFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MealPlans = context.MealPlans
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Title })
                    .ToList();

                return View(viewModel);
            }

            var item = new GroceryItem
            {
                Name = viewModel.Name,
                Quantity = viewModel.Quantity,
                IsPurchased = viewModel.IsPurchased,
                MealPlanId = viewModel.MealPlanId
            };

            context.GroceryItems.Add(item);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var item = context.GroceryItems.Find(id);
            if (item == null) return NotFound();

            var viewModel = new GroceryItemFormViewModel
            {
                Name = item.Name,
                Quantity = item.Quantity,
                IsPurchased = item.IsPurchased,
                MealPlanId = item.MealPlanId,
                MealPlans = context.MealPlans
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Title })
                    .ToList()
            };

            ViewBag.Id = id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GroceryItemFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MealPlans = context.MealPlans
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Title })
                    .ToList();

                ViewBag.Id = id;
                return View(viewModel);
            }

            var item = context.GroceryItems.Find(id);
            if (item == null) return NotFound();

            item.Name = viewModel.Name;
            item.Quantity = viewModel.Quantity;
            item.IsPurchased = viewModel.IsPurchased;
            item.MealPlanId = viewModel.MealPlanId;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var item = context.GroceryItems.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = context.GroceryItems.Find(id);
            if (item != null)
            {
                context.GroceryItems.Remove(item);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}