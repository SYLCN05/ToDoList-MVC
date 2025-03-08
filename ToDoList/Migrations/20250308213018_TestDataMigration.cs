using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class TestDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Taken",
                columns: new[] { "Id", "CreatedDateTime", "Description", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 8, 22, 30, 17, 914, DateTimeKind.Local).AddTicks(3158), "Dit is de beschrijving voor taak 1", "Test Taak1" },
                    { 2, new DateTime(2025, 3, 8, 22, 30, 17, 917, DateTimeKind.Local).AddTicks(301), "Dit is de beschrijving voor taak 2", "Test Taak2" },
                    { 3, new DateTime(2025, 3, 8, 22, 30, 17, 917, DateTimeKind.Local).AddTicks(321), "Dit is de beschrijving voor taak 3", "Test Taak3" },
                    { 4, new DateTime(2025, 3, 8, 22, 30, 17, 917, DateTimeKind.Local).AddTicks(325), "Je kunt het al raden dit is ook een bijscrijving, maar dan voor taak 4 :)", "Test Taak4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
