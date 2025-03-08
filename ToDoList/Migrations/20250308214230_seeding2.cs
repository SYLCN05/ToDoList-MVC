using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Je kunt het al raden dit is ook een beschrijving, maar dan voor taak 4 :)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Je kunt het al raden dit is ook een bijscrijving, maar dan voor taak 4 :)");
        }
    }
}
