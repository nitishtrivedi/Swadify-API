using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class NewTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Safe delete — won't fail if rows are already gone
            migrationBuilder.Sql(@"DELETE FROM ""MenuItems"" WHERE ""CategoryId"" IN (SELECT ""Id"" FROM ""MenuCategories"" WHERE ""RestaurantId"" = 0)");
            migrationBuilder.Sql(@"DELETE FROM ""MenuCategories"" WHERE ""RestaurantId"" = 0");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCategories_CategoryId",
                table: "MenuItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCategories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 83, DateTimeKind.Utc).AddTicks(7822), new DateTime(2026, 5, 7, 9, 11, 46, 83, DateTimeKind.Utc).AddTicks(7825) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1227), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1231) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1236), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1236) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1238), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1238) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1240), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1241), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1242) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1243), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1243) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1245), new DateTime(2026, 5, 7, 9, 11, 46, 84, DateTimeKind.Utc).AddTicks(1245) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCategories_CategoryId",
                table: "MenuItems");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCategories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(6396), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7592), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7593) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7595), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7595) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7597), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7597) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7598), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7598) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7599), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7599) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7600), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7601) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7602), new DateTime(2026, 5, 7, 9, 7, 55, 378, DateTimeKind.Utc).AddTicks(7602) });
        }
    }
}
