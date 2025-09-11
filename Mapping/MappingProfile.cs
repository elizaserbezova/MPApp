using AutoMapper;
using MealPlannerApp.Models;
using MealPlannerApp.ViewModels;

namespace MealPlannerApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meal, MealFormViewModel>().ReverseMap();
            CreateMap<MealPlan, MealPlanFormViewModel>().ReverseMap();
            CreateMap<GroceryItem, GroceryItemFormViewModel>().ReverseMap();
            CreateMap<Ingredient, IngredientFormViewModel>().ReverseMap();
            CreateMap<Recipe, RecipeFormViewModel>().ReverseMap();

        }
    }
}