using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AMS.DAL.Data.Migrations
{
    public partial class AMSDbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuName = table.Column<string>(nullable: true),
                    ParentID = table.Column<long>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuInfo_MenuInfo_ParentID",
                        column: x => x.ParentID,
                        principalTable: "MenuInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 20, nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreateUserID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 14, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserPic = table.Column<string>(maxLength: 100, nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    WorkUnit = table.Column<string>(maxLength: 30, nullable: true),
                    CreateUserID = table.Column<long>(nullable: false),
                    CreateUserTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuInModule",
                columns: table => new
                {
                    MenuID = table.Column<long>(nullable: false),
                    ModuleID = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuInModule", x => new { x.MenuID, x.ModuleID });
                    table.ForeignKey(
                        name: "FK_MenuInModule_MenuInfo_MenuID",
                        column: x => x.MenuID,
                        principalTable: "MenuInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleInMenu",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInMenu", x => new { x.RoleId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_RoleInMenu_RoleInfo_MenuId",
                        column: x => x.MenuId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleInMenu_MenuInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MenuInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleInModule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(nullable: false),
                    RoleInfoId = table.Column<long>(nullable: true),
                    ModuleID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleInModule_RoleInfo_RoleInfoId",
                        column: x => x.RoleInfoId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInRole",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserInRole_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInRole_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuInfo",
                columns: new[] { "Id", "MenuName", "ParentID", "Status" },
                values: new object[,]
                {
                    { 1L, "一级菜单1", null, 1 },
                    { 2L, "一级菜单2", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "RoleInfo",
                columns: new[] { "Id", "CreateUserID", "Description", "RoleName" },
                values: new object[] { 1L, 0L, null, "前台用户角色" });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "Id", "CreateUserID", "CreateUserTime", "Gender", "Name", "Password", "Status", "UserName", "UserPic", "UserType", "WorkUnit" },
                values: new object[,]
                {
                    { 1511L, 0L, new DateTime(2019, 10, 29, 15, 27, 33, 910, DateTimeKind.Local).AddTicks(7688), 0, null, "123456", 0, "admin", null, 1, null },
                    { 1512L, 0L, new DateTime(2019, 10, 29, 15, 27, 33, 913, DateTimeKind.Local).AddTicks(1200), 0, null, "123456", 0, "rxf", null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "MenuInModule",
                columns: new[] { "MenuID", "ModuleID", "Id" },
                values: new object[,]
                {
                    { 1L, 2L, 1L },
                    { 1L, 3L, 2L },
                    { 1L, 4L, 3L }
                });

            migrationBuilder.InsertData(
                table: "MenuInfo",
                columns: new[] { "Id", "MenuName", "ParentID", "Status" },
                values: new object[] { 3L, "一级菜单3", 1L, 1 });

            migrationBuilder.InsertData(
                table: "RoleInMenu",
                columns: new[] { "RoleId", "MenuId", "Id" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "UserInRole",
                columns: new[] { "UserId", "RoleId", "Id" },
                values: new object[] { 1512L, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_MenuInfo_ParentID",
                table: "MenuInfo",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInMenu_MenuId",
                table: "RoleInMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInModule_RoleInfoId",
                table: "RoleInModule",
                column: "RoleInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserName",
                table: "UserInfo",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInRole_RoleId",
                table: "UserInRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuInModule");

            migrationBuilder.DropTable(
                name: "RoleInMenu");

            migrationBuilder.DropTable(
                name: "RoleInModule");

            migrationBuilder.DropTable(
                name: "UserInRole");

            migrationBuilder.DropTable(
                name: "MenuInfo");

            migrationBuilder.DropTable(
                name: "RoleInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
