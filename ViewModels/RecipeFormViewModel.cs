using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.ViewModels
{
    public class RecipeFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Ingredients")]
        [DataType(DataType.MultilineText)]
        public string? Ingredients { get; set; }

        [Display(Name = "Preparation Instructions")]
        [DataType(DataType.MultilineText)]
        public string? Instructions { get; set; }

        [Display(Name = "Calories")]
        [Range(0, 10000, ErrorMessage = "Please enter a valid calorie value")]
        public int? Calories { get; set; }

        [Display(Name = "Preparation Time (minutes)")]
        [Range(0, 1440, ErrorMessage = "Enter a time between 0 and 1440 minutes")]
        public int? PreparationTime { get; set; }

        [Display(Name = "Created At")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime? CreatedAt { get; set; }
    }
}
