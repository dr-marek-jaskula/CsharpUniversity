using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "address");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "employee",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "customer",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "address");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "employee",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "customer",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "address",
                type: "int",
                nullable: true);
        }
    }
}
