using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealPlannerApp.Controllers
{
    public class MealPlanController : Controller
    {
        private readonly IMealPlanService _mealPlanService;
        private readonly IMealService _mealService;

        public MealPlanController(IMealPlanService mealPlanService, IMealService mealService)
        {
            _mealPlanService = mealPlanService;
            _mealService = mealService;
        }

        public IActionResult Index()
        {
            var plans = _mealPlanService.GetAll();
            return View(plans);
        }

        public IActionResult Details(int id)
        {
            var plan = _mealPlanService.GetById(id);
            if (plan == null) return NotFound();
            return View(plan);
        }

        public IActionResult Create()
        {
            var viewModel = new MealPlanFormViewModel
            {
                AllMeals = _mealService.GetAll()
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                    .ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MealPlanFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllMeals = _mealService.GetAll()
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                    .ToList();
                return View(viewModel);
            }

            // 🔁 Ръчно мапване към MealPlan
            var mealPlan = new MealPlan
            {
                Title = viewModel.Title,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };

            // Добавяне на избрани ястия
            var selectedMeals = _mealService
                .GetAll()
                .Where(m => viewModel.SelectedMealIds.Contains(m.Id))
                .ToList();

            mealPlan.Meals.AddRange(selectedMeals);

            _mealPlanService.Create(mealPlan);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var plan = _mealPlanService.GetById(id);
            if (plan == null) return NotFound();

            var viewModel = new MealPlanFormViewModel
            {
                Id = plan.Id,
                Title = plan.Title,
                StartDate = plan.StartDate,
                EndDate = plan.EndDate,
                SelectedMealIds = plan.Meals.Select(m => m.Id).ToList(),
                AllMeals = _mealService.GetAll()
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MealPlanFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AllMeals = _mealService.GetAll()
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                    .ToList();
                return View(viewModel);
            }

            var plan = _mealPlanService.GetById(viewModel.Id);
            if (plan == null) return NotFound();

            // 🔁 Ръчно обновяване на полетата
            plan.Title = viewModel.Title;
            plan.StartDate = viewModel.StartDate;
            plan.EndDate = viewModel.EndDate;

            // Обновяване на избраните ястия
            plan.Meals.Clear();
            var selectedMeals = _mealService
                .GetAll()
                .Where(m => viewModel.SelectedMealIds.Contains(m.Id))
                .ToList();

            plan.Meals.AddRange(selectedMeals);

            _mealPlanService.Update(plan);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var plan = _mealPlanService.GetById(id);
            if (plan == null) return NotFound();
            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _mealPlanService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}