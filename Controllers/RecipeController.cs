using AutoMapper;
using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeService recipeService, IMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            var viewModels = _mapper.Map<IEnumerable<RecipeFormViewModel>>(recipes);
            return View(viewModels);
        }


        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }
                
            var viewModel = _mapper.Map<RecipeFormViewModel>(recipe);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new RecipeFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var recipe = _mapper.Map<Recipe>(viewModel);
            await _recipeService.AddRecipeAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            
            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<RecipeFormViewModel>(recipe);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecipeFormViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var recipe = _mapper.Map<Recipe>(viewModel);
            await _recipeService.UpdateRecipeAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            
            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<RecipeFormViewModel>(recipe);
            return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
