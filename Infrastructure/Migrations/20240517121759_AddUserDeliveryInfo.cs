using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDeliveryInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("2a07b9b0-5a33-46f5-a2b7-2f5eb2744333"));

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("f03eba25-3860-46f6-a661-5d2c8d04db83"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c684178f-a323-4947-a7ff-3c725f349fa3"));

            migrationBuilder.CreateTable(
                name: "UserDeliveryInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeliveryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDeliveryInfos_Users_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserDeliveryInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { new Guid("ebb77204-e3ee-49fd-860b-d8220814e28a"), "courier@gmail.com", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" });

            migrationBuilder.InsertData(
                table: "DeliveryInfos",
                columns: new[] { "Id", "CourierId", "Date", "DeliveryInProgress", "PickUpInProgress" },
                values: new object[,]
                {
                    { new Guid("a74a729d-71ca-421f-8afb-de3232d38fef"), new Guid("ebb77204-e3ee-49fd-860b-d8220814e28a"), new DateOnly(2024, 5, 21), false, true },
                    { new Guid("ab2ae98c-09e7-4361-a90f-126bf0a036c5"), new Guid("ebb77204-e3ee-49fd-860b-d8220814e28a"), new DateOnly(2024, 5, 20), true, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDeliveryInfos_CourierId",
                table: "UserDeliveryInfos",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeliveryInfos_UserId",
                table: "UserDeliveryInfos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDeliveryInfos");

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("a74a729d-71ca-421f-8afb-de3232d38fef"));

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("ab2ae98c-09e7-4361-a90f-126bf0a036c5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ebb77204-e3ee-49fd-860b-d8220814e28a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { new Guid("c684178f-a323-4947-a7ff-3c725f349fa3"), "courier@gmail.com", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" });

            migrationBuilder.InsertData(
                table: "DeliveryInfos",
                columns: new[] { "Id", "CourierId", "Date", "DeliveryInProgress", "PickUpInProgress" },
                values: new object[,]
                {
                    { new Guid("2a07b9b0-5a33-46f5-a2b7-2f5eb2744333"), new Guid("c684178f-a323-4947-a7ff-3c725f349fa3"), new DateOnly(2024, 5, 21), false, true },
                    { new Guid("f03eba25-3860-46f6-a661-5d2c8d04db83"), new Guid("c684178f-a323-4947-a7ff-3c725f349fa3"), new DateOnly(2024, 5, 20), true, false }
                });
        }
    }
}
