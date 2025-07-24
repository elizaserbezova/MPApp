using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.ViewModels
{
    public class IngredientFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        [Display(Name = "Recipe")]
        public int RecipeId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Recipes { get; set; }
    }
}

