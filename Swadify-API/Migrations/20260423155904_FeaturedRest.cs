using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedRest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4184), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4190) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4803), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4804) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4833), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4833) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4835), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4835) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4836), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4836) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4837), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4837) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4838), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4839) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4840), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4840) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4841), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4841) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(5985), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(5990) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7139), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7139) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7141), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7141) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7142), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7143) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7144), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7144) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7145), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7146) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7147), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7147) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7148), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7148) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Restaurants");

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7240), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7241) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7764), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7765) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7766), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7767) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7767), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7768) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7768), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7769) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7770) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7771), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7771) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7772), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7772) });

            //migrationBuilder.UpdateData(
            //    table: "MenuCategories",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7773), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(7773) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(715), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(718) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1671), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1672) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1674), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1674) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1675), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1676) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1677), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1677) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1678), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1678) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1679), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1679) });

            //migrationBuilder.UpdateData(
            //    table: "RestaurantCategories",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    columns: new[] { "CreatedAt", "UpdatedAt" },
            //    values: new object[] { new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1680), new DateTime(2026, 4, 19, 4, 46, 36, 574, DateTimeKind.Utc).AddTicks(1680) });
        }
    }
}
