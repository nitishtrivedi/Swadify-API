using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationStatusAndAadhar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AadharNumber",
                table: "DeliveryPartnerProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatus",
                table: "DeliveryPartnerProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "DeliveryPartnerProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 412, DateTimeKind.Utc).AddTicks(9785), new DateTime(2026, 5, 19, 8, 0, 4, 412, DateTimeKind.Utc).AddTicks(9789) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(774), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(774) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(776), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(776) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(777), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(777) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(778), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(778) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(779), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(779) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(780), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(781), new DateTime(2026, 5, 19, 8, 0, 4, 413, DateTimeKind.Utc).AddTicks(782) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadharNumber",
                table: "DeliveryPartnerProfiles");

            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "DeliveryPartnerProfiles");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "DeliveryPartnerProfiles");

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(6597), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7642), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7642) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7643), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7644) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7645), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7645) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7646), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7646) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7647), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7647) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7648), new DateTime(2026, 5, 17, 13, 27, 56, 360, DateTimeKind.Utc).AddTicks(7649) });
        }
    }
}
