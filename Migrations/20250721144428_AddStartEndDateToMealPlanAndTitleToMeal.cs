using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlannerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStartEndDateToMealPlanAndTitleToMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDay",
                table: "MealPlans",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndDay",
                table: "MealPlans",
                newName: "EndDate");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MealPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MealPlans");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "MealPlans",
                newName: "StartDay");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "MealPlans",
                newName: "EndDay");
        }
    }
}
