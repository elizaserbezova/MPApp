using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MealPlannerApp.ViewModels
{
    public class MealPlanFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<int> SelectedMealIds { get; set; } = new List<int>();

        public List<SelectListItem> AllMeals { get; set; } = new List<SelectListItem>();
    }
}