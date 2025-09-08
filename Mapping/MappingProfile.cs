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
        }
    }
}