using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Data.Repositories.Interfaces;

namespace MealPlannerApp.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _repository;

        public MealService(IMealRepository repository)
        {
            _repository = repository;
        }

        public Meal GetById(int id) => _repository.GetById(id);

        public IEnumerable<Meal> GetAll() => _repository.GetAll();

        public void Create(Meal meal)
        {
            _repository.Add(meal);
            _repository.Save();
        }

        public void Update(Meal meal)
        {
            _repository.Update(meal);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var meal = _repository.GetById(id);
            if (meal != null)
            {
                _repository.Delete(meal);
                _repository.Save();
            }
        }
    }
}
