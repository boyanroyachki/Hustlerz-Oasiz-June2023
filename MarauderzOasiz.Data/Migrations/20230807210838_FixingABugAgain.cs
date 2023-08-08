using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HustlerzOasiz.Data.Migrations
{
    public partial class FixingABugAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("5005e172-6af8-48d2-a83a-116d688118da"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("734a3bb2-a3f5-4c80-97c5-5ca29968e426"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 8, 8, 0, 8, 37, 974, DateTimeKind.Local).AddTicks(3251), new DateTime(2023, 9, 22, 0, 8, 37, 974, DateTimeKind.Local).AddTicks(3312), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("734a3bb2-a3f5-4c80-97c5-5ca29968e426"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Jobs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "IsActive", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("5005e172-6af8-48d2-a83a-116d688118da"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 8, 4, 13, 20, 34, 629, DateTimeKind.Local).AddTicks(395), new DateTime(2023, 9, 18, 13, 20, 34, 629, DateTimeKind.Local).AddTicks(440), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, true, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }
    }
}
