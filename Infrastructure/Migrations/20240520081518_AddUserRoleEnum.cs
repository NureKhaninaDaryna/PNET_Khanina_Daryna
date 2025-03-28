using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT * FROM sys.columns 
                              WHERE Name = N'Role' AND Object_ID = Object_ID(N'Users'))
                BEGIN
                    ALTER TABLE Users ADD Role int DEFAULT 0
                END");
            
            migrationBuilder.Sql(@"
                IF EXISTS(SELECT * FROM sys.columns 
                          WHERE Name = N'Role' AND Object_ID = Object_ID(N'Users'))
                BEGIN
                    ALTER TABLE Users ALTER COLUMN Role int NULL
                END");
            
            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("d54d128f-0cc3-4d11-a623-37a9bf37ff2a"));

            migrationBuilder.DeleteData(
                table: "DeliveryInfos",
                keyColumn: "Id",
                keyValue: new Guid("f648897a-55fa-48a8-a06d-3b44517a30bd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfb65be0-8668-4b1e-9c1f-fd04263af35d"));

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("bfb65be0-8668-4b1e-9c1f-fd04263af35d"), "courier@gmail.com", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", 1 });

            migrationBuilder.InsertData(
                table: "DeliveryInfos",
                columns: new[] { "Id", "CourierId", "Date", "DeliveryInProgress", "PickUpInProgress" },
                values: new object[,]
                {
                    { new Guid("d54d128f-0cc3-4d11-a623-37a9bf37ff2a"), new Guid("bfb65be0-8668-4b1e-9c1f-fd04263af35d"), new DateOnly(2024, 5, 20), true, false },
                    { new Guid("f648897a-55fa-48a8-a06d-3b44517a30bd"), new Guid("bfb65be0-8668-4b1e-9c1f-fd04263af35d"), new DateOnly(2024, 5, 21), false, true }
                });
        }
    }
}
