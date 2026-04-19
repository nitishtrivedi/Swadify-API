using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingEnd2 : Migration
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
                    { 1, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1349), null, 1, null, true, "Starters", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1350) },
                    { 2, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1867), null, 2, null, true, "Main Course", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1867) },
                    { 3, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1869), null, 3, null, true, "Rice & Biryani", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1869) },
                    { 4, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1870), null, 4, null, true, "Breads", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1870) },
                    { 5, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1871), null, 5, null, true, "Soups & Salads", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1871) },
                    { 6, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1872), null, 6, null, true, "Desserts", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1872) },
                    { 7, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1873), null, 7, null, true, "Beverages", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1874) },
                    { 8, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1874), null, 8, null, true, "Fast Food", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1875) },
                    { 9, new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1875), null, 9, null, true, "Combo Meals", new DateTime(2026, 4, 19, 4, 40, 6, 758, DateTimeKind.Utc).AddTicks(1876) }
                });

            migrationBuilder.InsertData(
                table: "RestaurantCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IconUrl", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(4764), "Coffee and light bites", 1, null, true, "Café", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(4767) },
                    { 2, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5718), "Grills, BBQ and drinks", 2, null, true, "Bar & Grill", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5718) },
                    { 3, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5720), "Premium restaurant experience", 3, null, true, "Fine Dining", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5721) },
                    { 4, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5722), "Quick service restaurants", 4, null, true, "Fast Food", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5722) },
                    { 5, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5723), "Pizza and Italian food", 5, null, true, "Pizzeria", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5723) },
                    { 6, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5724), "Chinese cuisine", 6, null, true, "Chinese", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5724) },
                    { 7, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5725), "Indian cuisine", 7, null, true, "Indian", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5725) },
                    { 8, new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5726), "Breads, cakes and pastries", 8, null, true, "Bakery", new DateTime(2026, 4, 19, 4, 40, 6, 757, DateTimeKind.Utc).AddTicks(5727) }
                });
        }
    }
}
