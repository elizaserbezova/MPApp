using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlannerApp.Controllers
{
    public class GroceryItemController : Controller
    {
        private readonly IGroceryItemService _groceryItemService;
        private readonly IMealPlanService _mealPlanService;

        public GroceryItemController(IGroceryItemService groceryItemService, IMealPlanService mealPlanService)
        {
            _groceryItemService = groceryItemService;
            _mealPlanService = mealPlanService;
        }

        public IActionResult Index()
        {
            var items = _groceryItemService.GetAll();
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var item = _groceryItemService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create()
        {
            var viewModel = new GroceryItemFormViewModel
            {
                MealPlans = _mealPlanService.GetAll()
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
                viewModel.MealPlans = _mealPlanService.GetAll()
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

            _groceryItemService.Create(item);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var item = _groceryItemService.GetById(id);
            if (item == null) return NotFound();

            var viewModel = new GroceryItemFormViewModel
            {
                Name = item.Name,
                Quantity = item.Quantity,
                IsPurchased = item.IsPurchased,
                MealPlanId = item.MealPlanId,
                MealPlans = _mealPlanService.GetAll()
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
                viewModel.MealPlans = _mealPlanService.GetAll()
                    .Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Title })
                    .ToList();

                ViewBag.Id = id;
                return View(viewModel);
            }

            var item = _groceryItemService.GetById(id);
            if (item == null) return NotFound();

            item.Name = viewModel.Name;
            item.Quantity = viewModel.Quantity;
            item.IsPurchased = viewModel.IsPurchased;
            item.MealPlanId = viewModel.MealPlanId;

            _groceryItemService.Update(item);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var item = _groceryItemService.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _groceryItemService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}