using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class Tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "MenuItems",
                type: "text",
                nullable: false,
                defaultValue: "");


            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(7571), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(7575) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8770), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8771) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8773), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8773) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8774), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8775) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8776), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8776) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8777), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8777) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8778), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8779) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8780), new DateTime(2026, 5, 7, 8, 9, 39, 354, DateTimeKind.Utc).AddTicks(8780) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "MenuItems");

            

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(2984), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(2989) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4908), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4913), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4913) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4915), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4915) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4917), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4917) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4918), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4919) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4922), new DateTime(2026, 5, 7, 7, 45, 46, 419, DateTimeKind.Utc).AddTicks(4922) });
        }
    }
}
