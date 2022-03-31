using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class InitA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_ManagerId",
                table: "employee",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employee_ManagerId",
                table: "employee",
                column: "ManagerId",
                principalTable: "employee",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employee_ManagerId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_ManagerId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "employee");
        }
    }
}
