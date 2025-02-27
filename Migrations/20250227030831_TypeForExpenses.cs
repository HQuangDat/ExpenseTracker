using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class TypeForExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Expenses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Add");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Type",
                table: "Expenses",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expenses_Type",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Expenses");
        }
    }
}
