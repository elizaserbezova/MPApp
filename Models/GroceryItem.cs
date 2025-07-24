namespace MealPlannerApp.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Quantity { get; set; }

        public bool IsPurchased { get; set; }

        public int? MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
    }
}
