using AutoMapper;
using MealPlannerApp.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApp.Controllers
{
    public class MealPlanController : Controller
    {
        private readonly IMealPlanService _mealPlanService;
        private readonly IMapper _mapper;

        public MealPlanController(IMealPlanService mealPlanService, IMapper mapper)
        {
            _mealPlanService = mealPlanService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var mealPlans = await _mealPlanService.GetAllMealPlansAsync();
            return View(mealPlans);
        }

        public async Task<IActionResult> Details(int id)
        {
            var mealPlan = await _mealPlanService.GetMealPlanByIdAsync(id);
            if (mealPlan == null)
                return NotFound();

            return View(mealPlan);
        }

        public IActionResult Create()
        {
            return View(new MealPlanFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealPlanFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var mealPlan = _mapper.Map<MealPlan>(viewModel);
            await _mealPlanService.AddMealPlanAsync(mealPlan);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mealPlan = await _mealPlanService.GetMealPlanByIdAsync(id);
            if (mealPlan == null)
                return NotFound();

            var viewModel = _mapper.Map<MealPlanFormViewModel>(mealPlan);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MealPlanFormViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var mealPlan = _mapper.Map<MealPlan>(viewModel);
            await _mealPlanService.UpdateMealPlanAsync(mealPlan);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var mealPlan = await _mealPlanService.GetMealPlanByIdAsync(id);
            if (mealPlan == null)
                return NotFound();

            return View(mealPlan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mealPlanService.DeleteMealPlanAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
