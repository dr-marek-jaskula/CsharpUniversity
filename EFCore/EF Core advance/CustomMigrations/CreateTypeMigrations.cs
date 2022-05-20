using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Migrations
{
    public partial class CreateType : Migration
    {
        //Up method is performed when the database is updated (upgrade)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TYPE FilterTags AS TABLE ( Filter NVARCHAR(MAX) );");
        }

        //Down method is used to downgrade the migrations
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TYPE FilterTags;");
        }
    }
}