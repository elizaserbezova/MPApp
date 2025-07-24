using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MealPlannerApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Quantity { get; set; }

        public int RecipeId { get; set; }

        [ValidateNever]
        public Recipe Recipe { get; set; }

    }
}
