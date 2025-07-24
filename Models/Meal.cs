namespace MealPlannerApp.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TimeOfDay { get; set; } 

        
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int? MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
    }
}
