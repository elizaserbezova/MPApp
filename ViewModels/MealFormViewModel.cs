using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.ViewModels
{
    public class MealFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Time of Day")]
        public string TimeOfDay { get; set; }

        [Required]
        [Display(Name = "Recipe")]
        public int RecipeId { get; set; }

        public List<SelectListItem> Recipes { get; set; } = new();
    }
}