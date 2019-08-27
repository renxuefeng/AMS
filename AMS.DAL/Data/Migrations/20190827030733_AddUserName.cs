using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.DAL.Data.Migrations
{
    public partial class AddUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Log",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1511L,
                column: "CreateUserTime",
                value: new DateTime(2019, 8, 27, 11, 7, 33, 203, DateTimeKind.Local).AddTicks(4473));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Log");

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1511L,
                column: "CreateUserTime",
                value: new DateTime(2019, 8, 27, 11, 5, 16, 253, DateTimeKind.Local).AddTicks(3916));
        }
    }
}
