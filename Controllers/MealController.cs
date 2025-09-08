using AutoMapper;
using MealPlannerApp.Data;
using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;

        public MealController(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var meals = await _mealService.GetAllMealsAsync();
            return View(meals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
                return NotFound();

            return View(meal);
        }

        public IActionResult Create()
        {
            return View(new MealFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var meal = _mapper.Map<Meal>(viewModel);
            await _mealService.AddMealAsync(meal);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
                return NotFound();

            var viewModel = _mapper.Map<MealFormViewModel>(meal);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MealFormViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var meal = _mapper.Map<Meal>(viewModel);
            await _mealService.UpdateMealAsync(meal);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null)
                return NotFound();

            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mealService.DeleteMealAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}