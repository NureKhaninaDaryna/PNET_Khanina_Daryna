using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliferyInfoAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b5e4bab1-eaf3-4da2-96c0-06d2e92a9295"));

            migrationBuilder.CreateTable(
                name: "DeliveryInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CourierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryInProgress = table.Column<bool>(type: "bit", nullable: false),
                    PickUpInProgress = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryInfos_Users_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryInfos_CourierId",
                table: "DeliveryInfos",
                column: "CourierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryInfos");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c684178f-a323-4947-a7ff-3c725f349fa3"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { new Guid("b5e4bab1-eaf3-4da2-96c0-06d2e92a9295"), "courier@gmail.com", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" });
        }
    }
}
