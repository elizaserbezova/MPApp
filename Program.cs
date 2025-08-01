using MealPlannerApp.Data;
using MealPlannerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MealPlannerApp.Data.Repositories.Interfaces;
using MealPlannerApp.Data.Repositories;
using MealPlannerApp.Services.Interfaces;
using MealPlannerApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IMealPlanRepository, MealPlanRepository>();
builder.Services.AddScoped<IGroceryItemRepository, GroceryItemRepository>();

builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();
builder.Services.AddScoped<IGroceryItemService, GroceryItemService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

app.Run();