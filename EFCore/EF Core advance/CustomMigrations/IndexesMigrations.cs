using Microsoft.EntityFrameworkCore.Migrations;

//Its important to have this namespace here
namespace EFCore.Migrations
{
    //Custom migrations for adding Indexes (for CreateType look below)
    public partial class Indexes : Migration
    {
        private const string Index1 = "CREATE UNIQUE INDEX UX_Person_Email ON [dbo].[Person](Email) INCLUDE(FirstName, LastName);";
        private const string Index2 = "CREATE INDEX IX_Order_Deadline_Status ON [dbo].[Order](Deadline, Status) INCLUDE(Amount, ProductId) WHERE Status IN('Recieved', 'InProgress');";
        private const string Index3 = "CREATE INDEX IX_Payment_Deadline_Status ON [dbo].[Payment] (Deadline, Status) INCLUDE(Total) WHERE Status<> 'Rejected';";
        private const string Index4 = "CREATE UNIQUE INDEX IX_User_Username ON [dbo].[User](Username) INCLUDE(Email);";
        private const string Index5 = "CREATE UNIQUE INDEX IX_User_Email ON [dbo].[User](Email);";
        private const string Index6 = "CREATE UNIQUE INDEX IX_Person_Email ON [dbo].[Person](Email);";

        private const string DropIndex1 = "DROP INDEX UX_Person_Email;";
        private const string DropIndex2 = "DROP INDEX IX_Order_Deadline_Status;";
        private const string DropIndex3 = "DROP INDEX IX_Payment_Deadline_Status;";
        private const string DropIndex4 = "DROP INDEX IX_User_Username;";
        private const string DropIndex5 = "DROP INDEX IX_User_Email;";
        private const string DropIndex6 = "DROP INDEX IX_Person_Email;";

        //Up method is performed when the database is updated (upgrade)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{Index1}{Index2}{Index3}{Index4}{Index5}{Index6}");
        }

        //Down method is used to downgrade the migrations
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{DropIndex1}{DropIndex2}{DropIndex3}{DropIndex4}{DropIndex5}{DropIndex6}");
        }
    }
}