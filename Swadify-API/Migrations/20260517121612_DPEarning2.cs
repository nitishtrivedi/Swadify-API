using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class DPEarning2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPartnerEarning_Orders_OrderId",
                table: "DeliveryPartnerEarning");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPartnerEarning_Users_DeliveryPartnerId",
                table: "DeliveryPartnerEarning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryPartnerEarning",
                table: "DeliveryPartnerEarning");

            migrationBuilder.RenameTable(
                name: "DeliveryPartnerEarning",
                newName: "DeliveryPartnerEarnings");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryPartnerEarning_OrderId",
                table: "DeliveryPartnerEarnings",
                newName: "IX_DeliveryPartnerEarnings_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryPartnerEarning_DeliveryPartnerId",
                table: "DeliveryPartnerEarnings",
                newName: "IX_DeliveryPartnerEarnings_DeliveryPartnerId");

            migrationBuilder.AddColumn<decimal>(
                name: "PendingEarnings",
                table: "DeliveryPartnerProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarnings",
                table: "DeliveryPartnerProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WithdrawnEarnings",
                table: "DeliveryPartnerProfiles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryPartnerEarnings",
                table: "DeliveryPartnerEarnings",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPartnerEarnings_Orders_OrderId",
                table: "DeliveryPartnerEarnings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPartnerEarnings_Users_DeliveryPartnerId",
                table: "DeliveryPartnerEarnings",
                column: "DeliveryPartnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPartnerEarnings_Orders_OrderId",
                table: "DeliveryPartnerEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryPartnerEarnings_Users_DeliveryPartnerId",
                table: "DeliveryPartnerEarnings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryPartnerEarnings",
                table: "DeliveryPartnerEarnings");

            migrationBuilder.DropColumn(
                name: "PendingEarnings",
                table: "DeliveryPartnerProfiles");

            migrationBuilder.DropColumn(
                name: "TotalEarnings",
                table: "DeliveryPartnerProfiles");

            migrationBuilder.DropColumn(
                name: "WithdrawnEarnings",
                table: "DeliveryPartnerProfiles");

            migrationBuilder.RenameTable(
                name: "DeliveryPartnerEarnings",
                newName: "DeliveryPartnerEarning");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryPartnerEarnings_OrderId",
                table: "DeliveryPartnerEarning",
                newName: "IX_DeliveryPartnerEarning_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryPartnerEarnings_DeliveryPartnerId",
                table: "DeliveryPartnerEarning",
                newName: "IX_DeliveryPartnerEarning_DeliveryPartnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryPartnerEarning",
                table: "DeliveryPartnerEarning",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(6591), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(6597) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8579), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8580) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8584), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8585) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8586), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8587) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8588), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8588) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8590), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8591) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8592), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8593) });

            migrationBuilder.UpdateData(
                table: "RestaurantCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8594), new DateTime(2026, 5, 17, 12, 5, 39, 554, DateTimeKind.Utc).AddTicks(8594) });

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPartnerEarning_Orders_OrderId",
                table: "DeliveryPartnerEarning",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryPartnerEarning_Users_DeliveryPartnerId",
                table: "DeliveryPartnerEarning",
                column: "DeliveryPartnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
