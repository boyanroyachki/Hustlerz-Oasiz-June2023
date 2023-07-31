using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HustlerzOasiz.Data.Migrations
{
    public partial class isActivePropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("c32c9662-7ed6-4396-a454-f26618249af8"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "IsActive", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("89ce087e-894a-4f4e-ae0f-9d8542594465"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 7, 31, 21, 29, 19, 938, DateTimeKind.Local).AddTicks(7409), new DateTime(2023, 9, 14, 21, 29, 19, 938, DateTimeKind.Local).AddTicks(7481), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, false, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("89ce087e-894a-4f4e-ae0f-9d8542594465"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Jobs");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ContractorId", "DatePosted", "Deadline", "Details", "ExecutorId", "ImageURLs", "Location", "Price", "Status", "Title" },
                values: new object[] { new Guid("c32c9662-7ed6-4396-a454-f26618249af8"), 2, new Guid("5bb27ea6-15ab-4602-b761-1cd6d4347356"), new DateTime(2023, 7, 17, 13, 30, 23, 369, DateTimeKind.Local).AddTicks(9812), new DateTime(2023, 8, 31, 13, 30, 23, 369, DateTimeKind.Local).AddTicks(9881), "Train local forces in advanced defensive tactics to counter potential threats. Requires expertise in urban warfare strategies.", new Guid("eab58257-2186-4f8c-97e3-08db86032dab"), null, "Middle East", 20000m, "Active", "Defensive Maneuvers" });
        }
    }
}
