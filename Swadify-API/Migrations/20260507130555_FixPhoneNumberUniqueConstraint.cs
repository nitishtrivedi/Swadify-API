using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class FixPhoneNumberUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 246, DateTimeKind.Utc).AddTicks(8279), new DateTime(2026, 5, 7, 13, 5, 54, 246, DateTimeKind.Utc).AddTicks(8285) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(210), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(211) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(214), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(215) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(217), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(217) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(218), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(219) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(220), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(220) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(222), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(222) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(224), new DateTime(2026, 5, 7, 13, 5, 54, 247, DateTimeKind.Utc).AddTicks(224) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true,
                filter: "\"PhoneNumber\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
