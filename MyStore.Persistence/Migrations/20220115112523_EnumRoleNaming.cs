using Microsoft.EntityFrameworkCore.Migrations;

namespace MyStore.Persistence.Migrations
{
    public partial class EnumRoleNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "101220ab-1803-4c29-8c72-cbf2a4cdebd9");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3294feea-6baa-489d-bdf4-47a01ab96564");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bae574fb-af68-4633-a3af-8a8f3671eede");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bebfa6a-e196-4253-b77c-468380f4c652", "3817cd7e-f9ee-4fba-8dfd-5128fa0f3343", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2032f83a-36cd-4217-9ab8-2fa0de3d53e5", "45519dfe-03f8-4d5e-89f7-a31ad2be8d5b", "Operator", "OPERATOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6070e5b-5a81-4fa3-a002-26110021c697", "492777b9-69a0-4f9d-a3c1-4b4390820deb", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0bebfa6a-e196-4253-b77c-468380f4c652");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2032f83a-36cd-4217-9ab8-2fa0de3d53e5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b6070e5b-5a81-4fa3-a002-26110021c697");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3294feea-6baa-489d-bdf4-47a01ab96564", "b8c684ea-2f62-4c1e-8c8b-f68f55f116ac", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "101220ab-1803-4c29-8c72-cbf2a4cdebd9", "96c2cd4c-b452-45f8-91a4-3f80b2d01993", "Operator", "OPERATOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bae574fb-af68-4633-a3af-8a8f3671eede", "fd659e3e-7df7-4a1c-ab04-6259fc018db4", "User", "USER" });
        }
    }
}
