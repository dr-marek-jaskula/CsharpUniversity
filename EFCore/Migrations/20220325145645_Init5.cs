using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_salary_salary_transfer_SalaryTransferId",
                table: "salary");

            migrationBuilder.DropForeignKey(
                name: "FK_salary_transfer_employee_EmployeeId",
                table: "salary_transfer");

            migrationBuilder.DropIndex(
                name: "IX_salary_SalaryTransferId",
                table: "salary");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "salary_transfer",
                newName: "SalaryId");

            migrationBuilder.RenameIndex(
                name: "IX_salary_transfer_EmployeeId",
                table: "salary_transfer",
                newName: "IX_salary_transfer_SalaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_salary_transfer_salary_SalaryId",
                table: "salary_transfer",
                column: "SalaryId",
                principalTable: "salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_salary_transfer_salary_SalaryId",
                table: "salary_transfer");

            migrationBuilder.RenameColumn(
                name: "SalaryId",
                table: "salary_transfer",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_salary_transfer_SalaryId",
                table: "salary_transfer",
                newName: "IX_salary_transfer_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_salary_SalaryTransferId",
                table: "salary",
                column: "SalaryTransferId",
                unique: true,
                filter: "[SalaryTransferId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_salary_salary_transfer_SalaryTransferId",
                table: "salary",
                column: "SalaryTransferId",
                principalTable: "salary_transfer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_salary_transfer_employee_EmployeeId",
                table: "salary_transfer",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
