using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HustlerzOasiz.Data.Migrations
{
    public partial class ArchiveAndUserUsernameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("5005e172-6af8-48d2-a83a-116d688118da"));

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AspNetUsers",
                newName: "Username");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "DefaultName");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("f794ad20-55c7-494d-b060-4502054ee88a"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 8, 7, 15, 58, 43, 465, DateTimeKind.Local).AddTicks(6131), new DateTime(2023, 9, 21, 15, 58, 43, 465, DateTimeKind.Local).AddTicks(6208), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("f794ad20-55c7-494d-b060-4502054ee88a"));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Jobs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "IsActive", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("5005e172-6af8-48d2-a83a-116d688118da"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 8, 4, 13, 20, 34, 629, DateTimeKind.Local).AddTicks(395), new DateTime(2023, 9, 18, 13, 20, 34, 629, DateTimeKind.Local).AddTicks(440), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, true, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }
    }
}
