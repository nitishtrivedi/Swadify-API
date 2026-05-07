using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class IsActiveMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MenuItems",
                type: "boolean",
                nullable: false,
                defaultValue: true); // default true so existing items stay visible

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "MenuCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Delete the seeded MenuCategories — they are generic categories
            // (Starters, Main Course etc.) that don't belong to any restaurant.
            // Real categories will be created per-restaurant via the admin UI.
            migrationBuilder.Sql(@"DELETE FROM ""MenuCategories""");

            // Reset the sequence so new categories start from 1
            migrationBuilder.Sql(@"SELECT setval(pg_get_serial_sequence('""MenuCategories""', 'Id'), 1, false)");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_RestaurantId",
                table: "MenuCategories",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_Restaurants_RestaurantId",
                table: "MenuCategories",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_Restaurants_RestaurantId",
                table: "MenuCategories");

            migrationBuilder.DropIndex(
                name: "IX_MenuCategories_RestaurantId",
                table: "MenuCategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "MenuCategories");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4184), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4803), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4804) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4833), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4833) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4835), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4835) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4836), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4836) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4837), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4837) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4838), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4839) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4840), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4841), new DateTime(2026, 4, 23, 15, 59, 3, 233, DateTimeKind.Utc).AddTicks(4841) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(5985), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7139), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7139) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7141), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7141) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7142), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7144), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7144) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7145), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7146) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7147), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7147) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7148), new DateTime(2026, 4, 23, 15, 59, 3, 232, DateTimeKind.Utc).AddTicks(7148) });
        }
    }
}
