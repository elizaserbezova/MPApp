using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;

namespace MealPlannerApp.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;

        public IngredientService(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public Ingredient GetById(int id) => _repository.GetById(id);

        public IEnumerable<Ingredient> GetAll() => _repository.GetAll();

        public void Create(Ingredient ingredient)
        {
            _repository.Add(ingredient);
            _repository.Save();
        }

        public void Update(Ingredient ingredient)
        {
            _repository.Update(ingredient);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var ingredient = _repository.GetById(id);
            if (ingredient != null)
            {
                _repository.Delete(ingredient);
                _repository.Save();
            }
        }
    }
}
