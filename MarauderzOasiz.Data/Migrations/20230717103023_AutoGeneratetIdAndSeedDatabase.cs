using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HustlerzOasiz.Data.Migrations
{
    public partial class AutoGeneratetIdAndSeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Ammonition" },
                    { 2, null, "Protection" },
                    { 3, null, "Special Mission" },
                    { 4, null, "Cyber warfare/ Cyber security" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("c32c9662-7ed6-4396-a454-f26618249af8"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 7, 17, 13, 30, 23, 369, DateTimeKind.Local).AddTicks(9812), new DateTime(2023, 8, 31, 13, 30, 23, 369, DateTimeKind.Local).AddTicks(9881), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("c32c9662-7ed6-4396-a454-f26618249af8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
