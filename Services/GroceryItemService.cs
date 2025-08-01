using MealPlannerApp.Models;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Data.Repositories.Interfaces;

namespace MealPlannerApp.Services
{
    public class GroceryItemService : IGroceryItemService
    {
        private readonly IGroceryItemRepository _repository;

        public GroceryItemService(IGroceryItemRepository repository)
        {
            _repository = repository;
        }

        public GroceryItem GetById(int id) => _repository.GetById(id);

        public IEnumerable<GroceryItem> GetAll() => _repository.GetAll();

        public void Create(GroceryItem item)
        {
            _repository.Add(item);
            _repository.Save();
        }

        public void Update(GroceryItem item)
        {
            _repository.Update(item);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var item = _repository.GetById(id);
            if (item != null)
            {
                _repository.Delete(item);
                _repository.Save();
            }
        }
    }
}