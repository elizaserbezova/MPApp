using AutoMapper;
using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers
{
    public class GroceryItemController : Controller
    {
        private readonly IGroceryItemService _groceryItemService;
        private readonly IMapper _mapper;

        public GroceryItemController(IGroceryItemService groceryItemService, IMapper mapper)
        {
            _groceryItemService = groceryItemService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _groceryItemService.GetAllGroceryItemsAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _groceryItemService.GetGroceryItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        public IActionResult Create()
        {
            return View(new GroceryItemFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroceryItemFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var item = _mapper.Map<GroceryItem>(viewModel);
            await _groceryItemService.AddGroceryItemAsync(item);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _groceryItemService.GetGroceryItemByIdAsync(id);
            if (item == null)
                return NotFound();

            var viewModel = _mapper.Map<GroceryItemFormViewModel>(item);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroceryItemFormViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var item = _mapper.Map<GroceryItem>(viewModel);
            await _groceryItemService.UpdateGroceryItemAsync(item);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _groceryItemService.GetGroceryItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _groceryItemService.DeleteGroceryItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
