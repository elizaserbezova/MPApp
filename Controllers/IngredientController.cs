using AutoMapper;
using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientService ingredientService, IMapper mapper)
        {
            _ingredientService = ingredientService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return View(ingredients);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        public IActionResult Create()
        {
            return View(new IngredientFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var ingredient = _mapper.Map<Ingredient>(viewModel);
            await _ingredientService.AddIngredientAsync(ingredient);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
                return NotFound();

            var viewModel = _mapper.Map<IngredientFormViewModel>(ingredient);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IngredientFormViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var ingredient = _mapper.Map<Ingredient>(viewModel);
            await _ingredientService.UpdateIngredientAsync(ingredient);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
