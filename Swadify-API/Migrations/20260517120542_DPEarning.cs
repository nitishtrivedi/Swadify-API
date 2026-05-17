using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Swadify_API.Migrations
{
    /// <inheritdoc />
    public partial class DPEarning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryPartnerEarning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeliveryPartnerId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "numeric", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PenaltyAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    NetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPaidOut = table.Column<bool>(type: "boolean", nullable: false),
                    EarnedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPartnerEarning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryPartnerEarning_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryPartnerEarning_Users_DeliveryPartnerId",
                        column: x => x.DeliveryPartnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPartnerEarning_DeliveryPartnerId",
                table: "DeliveryPartnerEarning",
                column: "DeliveryPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPartnerEarning_OrderId",
                table: "DeliveryPartnerEarning",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryPartnerEarning");

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
        }
    }
}
