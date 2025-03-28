using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class decorator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHighlighted",
                table: "Taken",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsHighlighted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsHighlighted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsHighlighted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Taken",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsHighlighted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHighlighted",
                table: "Taken");
        }
    }
}
