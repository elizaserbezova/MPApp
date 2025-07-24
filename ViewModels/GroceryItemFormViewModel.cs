using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.ViewModels
{
    public class GroceryItemFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }

        public bool IsPurchased { get; set; }

        public int? MealPlanId { get; set; }

        public List<SelectListItem> MealPlans { get; set; } = new List<SelectListItem>();
    }
}
