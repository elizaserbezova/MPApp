using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;

public class MealPlanService : IMealPlanService
{
    private readonly IMealPlanRepository _repository;

    public MealPlanService(IMealPlanRepository repository)
    {
        _repository = repository;
    }

    public MealPlan GetById(int id) => _repository.GetById(id);

    public IEnumerable<MealPlan> GetAll() => _repository.GetAll();

    public void Create(MealPlan mealPlan)
    {
        _repository.Add(mealPlan);
        _repository.Save();
    }

    public void Update(MealPlan mealPlan)
    {
        _repository.Update(mealPlan);
        _repository.Save();
    }

    public void Delete(int id)
    {
        var mealPlan = _repository.GetById(id);
        if (mealPlan != null)
        {
            _repository.Delete(mealPlan);
            _repository.Save();
        }
    }
}
