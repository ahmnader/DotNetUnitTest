using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevOpsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class rename_to_employee_column_fullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToDoName",
                table: "Employees",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employees",
                newName: "ToDoName");
        }
    }
}
