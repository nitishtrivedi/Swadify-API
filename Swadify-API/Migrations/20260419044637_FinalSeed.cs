using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IconUrl", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7240), null, 1, null, true, "Starters", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7241) },
                    { 2, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7764), null, 2, null, true, "Main Course", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7765) },
                    { 3, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7766), null, 3, null, true, "Rice & Biryani", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7767) },
                    { 4, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7767), null, 4, null, true, "Breads", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7768) },
                    { 5, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7768), null, 5, null, true, "Soups & Salads", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7769) },
                    { 6, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7770), null, 6, null, true, "Desserts", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7770) },
                    { 7, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7771), null, 7, null, true, "Beverages", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7771) },
                    { 8, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7772), null, 8, null, true, "Fast Food", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7772) },
                    { 9, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7773), null, 9, null, true, "Combo Meals", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7773) }
                });

            migrationBuilder.InsertData(
                table: "RestaurantCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IconUrl", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(715), "Coffee and light bites", 1, null, true, "Café", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(718) },
                    { 2, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1671), "Grills, BBQ and drinks", 2, null, true, "Bar & Grill", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1672) },
                    { 3, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1674), "Premium restaurant experience", 3, null, true, "Fine Dining", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1674) },
                    { 4, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1675), "Quick service restaurants", 4, null, true, "Fast Food", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1676) },
                    { 5, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1677), "Pizza and Italian food", 5, null, true, "Pizzeria", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1677) },
                    { 6, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1678), "Chinese cuisine", 6, null, true, "Chinese", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1678) },
                    { 7, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1679), "Indian cuisine", 7, null, true, "Indian", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1679) },
                    { 8, new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1680), "Breads, cakes and pastries", 8, null, true, "Bakery", new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1680) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
