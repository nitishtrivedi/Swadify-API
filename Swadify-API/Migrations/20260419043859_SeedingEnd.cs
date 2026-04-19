using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingEnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IconUrl", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2307), null, 1, null, true, "Starters", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2307) },
                    { 2, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2877), null, 2, null, true, "Main Course", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2877) },
                    { 3, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2879), null, 3, null, true, "Rice & Biryani", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2879) },
                    { 4, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2880), null, 4, null, true, "Breads", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2880) },
                    { 5, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2881), null, 5, null, true, "Soups & Salads", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2881) },
                    { 6, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2882), null, 6, null, true, "Desserts", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2882) },
                    { 7, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2883), null, 7, null, true, "Beverages", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2883) },
                    { 8, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2884), null, 8, null, true, "Fast Food", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2884) },
                    { 9, new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2885), null, 9, null, true, "Combo Meals", new DateTime(2026, 4, 19, 4, 23, 7, 176, DateTimeKind.Utc).AddTicks(2885) }
                });

            migrationBuilder.InsertData(
                table: "RestaurantCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IconUrl", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(5232), "Coffee and light bites", 1, null, true, "Café", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(5236) },
                    { 2, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6276), "Grills, BBQ and drinks", 2, null, true, "Bar & Grill", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6277) },
                    { 3, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6279), "Premium restaurant experience", 3, null, true, "Fine Dining", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6279) },
                    { 4, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6280), "Quick service restaurants", 4, null, true, "Fast Food", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6280) },
                    { 5, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6281), "Pizza and Italian food", 5, null, true, "Pizzeria", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6282) },
                    { 6, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6282), "Chinese cuisine", 6, null, true, "Chinese", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6283) },
                    { 7, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6284), "Indian cuisine", 7, null, true, "Indian", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6284) },
                    { 8, new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6285), "Breads, cakes and pastries", 8, null, true, "Bakery", new DateTime(2026, 4, 19, 4, 23, 7, 175, DateTimeKind.Utc).AddTicks(6285) }
                });
        }
    }
}
