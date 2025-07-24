namespace MealPlannerApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int Calories { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
