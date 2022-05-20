using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Migrations
{
    public partial class AddView : Migration
    {
        //Name of the view needs to fit the name given in the MyDbContext
        private const string createView = @"
        CREATE VIEW View_EmployeeContactData AS
        SELECT p.FirstName, p.LastName, p.ContactNumber, p.Email, m.FirstName + ' ' + m.LastName AS Manager
        FROM [Employee] AS e
        JOIN[Person] AS p ON e.Id = p.Id
        JOIN[Person] AS m ON e.ManagerId = m.Id;
        ";

        private const string deleteView = @"
        DROP VIEW View_EmployeeContactData;
        ";

        //Up method is performed when the database is updated (upgrade)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{createView}");
        }

        //Down method is used to downgrade the migrations
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{deleteView}");
        }
    }
}