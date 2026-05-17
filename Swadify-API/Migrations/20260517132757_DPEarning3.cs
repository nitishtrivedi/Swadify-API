using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class DPEarning3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryAcceptedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryAssignedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryAssignmentStatus",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryRejectedAt",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAcceptedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAssignedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAssignmentStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryRejectedAt",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(5299), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(5303) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6780), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6780) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6782), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6783) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6784), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6784) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6785), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6786) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6789), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6789) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6790), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6791) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6792), new DateTime(2026, 5, 17, 12, 16, 10, 875, DateTimeKind.Utc).AddTicks(6792) });
        }
    }
}
