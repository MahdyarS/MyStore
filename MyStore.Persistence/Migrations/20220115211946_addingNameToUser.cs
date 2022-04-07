using Microsoft.EntityFrameworkCore.Migrations;

namespace MyStore.Persistence.Migrations
{
    public partial class addingNameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12da1dc8-142e-423b-be76-04bf80404cc8", "4966fb41-738a-4fad-8871-567fc726e500", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af767695-8098-41e6-91e2-92cec6b90abd", "fc918128-946b-4b05-bffa-191ae89bb1a2", "Operator", "OPERATOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04c04491-919f-4aea-8be0-7dcc48157583", "d1d29a8e-fbed-41da-a285-0b342e2e255d", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "04c04491-919f-4aea-8be0-7dcc48157583");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "12da1dc8-142e-423b-be76-04bf80404cc8");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "af767695-8098-41e6-91e2-92cec6b90abd");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

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
    }
}
