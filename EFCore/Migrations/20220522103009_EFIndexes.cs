using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class EFIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_Person_Email",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Person_Email",
                table: "Person",
                column: "Email",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Deadline_Status",
                table: "Payment",
                columns: new[] { "Deadline", "Status" },
                filter: "Status <> 'Rejected'")
                .Annotation("SqlServer:Include", new[] { "Total" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Deadline_Status",
                table: "Order",
                columns: new[] { "Deadline", "Status" },
                filter: "Status IN ('Received', 'InProgress')")
                .Annotation("SqlServer:Include", new[] { "Amount", "ProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Person_Email",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "UX_Person_Email",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Payment_Deadline_Status",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Order_Deadline_Status",
                table: "Order");
        }
    }
}
