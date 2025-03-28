using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProfileProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("899dd355-f590-4873-815b-7e68291a5044"));

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("d549608e-4edb-4b04-9591-1f095708cd05"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d43dc96c-ccab-43ae-85a5-e55c1f024172"));

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DayOfBirth",
                table: "Users",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfileImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImage", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarId", "DayOfBirth", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Rating", "Role", "UserName" },
                values: new object[] { new Guid("e690231a-5572-43f8-b42a-0f37a11a3567"), null, null, "courier@gmail.com", null, null, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", null, null, 1, null });

            migrationBuilder.InsertData(
                table: "DeliveryInfos",
                columns: new[] { "Id", "CourierId", "Date", "DeliveryInProgress", "PickUpInProgress" },
                values: new object[,]
                {
                    { new Guid("8b54d58f-4cc7-4ae7-8af6-38395ec43d2d"), new Guid("e690231a-5572-43f8-b42a-0f37a11a3567"), new DateOnly(2024, 5, 20), true, false },
                    { new Guid("e7bfba54-d91d-4f69-a27b-32a755454475"), new Guid("e690231a-5572-43f8-b42a-0f37a11a3567"), new DateOnly(2024, 5, 21), false, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ProfileImage_AvatarId",
                table: "Users",
                column: "AvatarId",
                principalTable: "ProfileImage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ProfileImage_AvatarId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ProfileImage");

            migrationBuilder.DropIndex(
                name: "IX_Users_AvatarId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("8b54d58f-4cc7-4ae7-8af6-38395ec43d2d"));

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("e7bfba54-d91d-4f69-a27b-32a755454475"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e690231a-5572-43f8-b42a-0f37a11a3567"));

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DayOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("d43dc96c-ccab-43ae-85a5-e55c1f024172"), "courier@gmail.com", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", 1 });

            migrationBuilder.InsertData(
                table: "DeliveryInfos",
                columns: new[] { "Id", "CourierId", "Date", "DeliveryInProgress", "PickUpInProgress" },
                values: new object[,]
                {
                    { new Guid("899dd355-f590-4873-815b-7e68291a5044"), new Guid("d43dc96c-ccab-43ae-85a5-e55c1f024172"), new DateOnly(2024, 5, 21), false, true },
                    { new Guid("d549608e-4edb-4b04-9591-1f095708cd05"), new Guid("d43dc96c-ccab-43ae-85a5-e55c1f024172"), new DateOnly(2024, 5, 20), true, false }
                });
        }
    }
}
