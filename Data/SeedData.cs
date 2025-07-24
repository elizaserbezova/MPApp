using MealPlannerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Ако вече има данни - спри
            if (context.Recipes.Any() || context.MealPlans.Any())
            {
                return;
            }

            var recipes = new[]
            {
                new Recipe { Name = "Овесена каша", Ingredients = "овес, мляко, мед", Instructions = "Свари овеса", Calories = 250 },
                new Recipe { Name = "Пилешка салата", Ingredients = "пиле, маруля, домат", Instructions = "Смеси всичко", Calories = 300 },
                new Recipe { Name = "Смути", Ingredients = "банан, мляко, спанак", Instructions = "Блендирай всичко", Calories = 200 }
            };
            context.Recipes.AddRange(recipes);
            context.SaveChanges();

 
            var mealPlan = new MealPlan
            {
                Title = "Примерен план",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(6),
                Meals = new List<Meal>
                {
                    new Meal { Name = "Закуска", Date = DateTime.Today, TimeOfDay = "Сутрин", RecipeId = recipes[0].Id },
                    new Meal { Name = "Обяд", Date = DateTime.Today, TimeOfDay = "Обяд", RecipeId = recipes[1].Id },
                    new Meal { Name = "Вечеря", Date = DateTime.Today, TimeOfDay = "Вечер", RecipeId = recipes[2].Id }
                }
            };
            context.MealPlans.Add(mealPlan);
            context.SaveChanges();
        }
    }
}
    

