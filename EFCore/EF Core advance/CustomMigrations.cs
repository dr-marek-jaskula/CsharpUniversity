using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.EF_Core_advance
{
    //In order to implement this approach we need to manually change the migration file!

    //Up method is the method that will apply changes to the database when "dotnet ef database update"
    //Down method is the method that will apply changes to the database when "dotnet ef database update <name_of_previous_migrations>"

    public class CustomMigrations
    {
        //Multi criteria search by LINQ is hard or impossible
        //Therefore, we should use raw SQL

        #region RAW SQR for multi criteria search

        /*
        --  At first create a Table-Valued Parameter (a new type) [IN ORDER TO DO THIS BY ENTITY FRAMEWORK CORE WE NEED TO MANYALLY CHANGE THE MIGRATION!]
            DROP TYPE FilterTags;
            CREATE TYPE FilterTags AS TABLE ( Filter NVARCHAR(MAX) );

            DECLARE @Filters FilterTags
            INSERT INTO @Filters VALUES('%chair%');
            INSERT INTO @Filters VALUES('%plastic%');

        -- The first query(Hits) gives 0 or 1 for each filter.
        -- For example we can 3 offerings: 1,2,3 and 2 filters: chair, plastic
        -- Then after CROSS JOIN we have: "1 chair", "1 plastic", "2 chair", "2 plastic", "3 chair", "3 plastic"
        -- Next we use CASE WHEN to set 1 or 0 to this elements (if in Title or description or tag there are such filters)
        -- In second query(FilteredHints) we summarize the results from previous query(these 1 and 0) and filter by these that match at least 1
        -- In thirds query we get the Offerings and sort them by number of accurate hints(by this sums -> more matches, record is first)

        WITH Hits AS(
            SELECT o.Id,
                    CASE WHEN
                        o.Title LIKE f.Filter
                        OR o.[Description] LIKE f.Filter
                        OR EXISTS (
                            SELECT  1
                            FROM OfferingTags ot
                                    INNER JOIN OfferingsTags ots ON ot.Id = ots.TagsId
                            WHERE ots.OfferingsId = o.Id
                                    AND ot.Tag LIKE f.Filter
                        )
                        THEN 1
                        ELSE 0
                    END AS ContainsFilter
            FROM    Offerings o
                    CROSS JOIN @Filters f
        ),
        FilteredHits AS(
            SELECT h.Id,
                    SUM(h.ContainsFilter) AS Hits
            FROM    Hits h
            GROUP BY h.Id
            HAVING  SUM(h.ContainsFilter) > 0
        )
        SELECT o.Id,
                o.Title,
                o.[Description]
        FROM    Offerings o
                INNER JOIN FilteredHits fh ON o.Id = fh.Id
        ORDER BY fh.Hits DESC

            */

        #endregion RAW SQR for multi criteria search

        //To Table-Valued Parameter in c#.
        //The solution is "DataTable" class
        //Example:
        //To Table-Valued Parameter in c#. The solution is "DataTable" class
        //var filterTable = new DataTable();
        //filterTable.Columns.Add("Filter", typeof(string));
    }
}

//Now, to change the migrations:
//1. We create a new migrations: "dotnet ef migrations add CreateType"
//2. Go to migrations -> clean all the migration class (delete everything inside the class)
//3. Copy it to the new class (for simplicity we will keep it here in scope-range namespace)
//4. We can change it here because the class is partial.
//5. Override up and down methods (remember to remove them from the migrations folder)
//6. Make migrations

namespace EFCore.Migrations
{
    //Custom migrations for adding Indexes (for CreateType look below)
    public partial class Indexes : Migration
    {
        private const string Index1 = "CREATE UNIQUE INDEX UX_Employee_Email ON [dbo].[Employee](Email) INCLUDE(FirstName, LastName);";
        private const string Index2 = "CREATE UNIQUE INDEX UX_Customer_Email ON [dbo].[Customer](Email) INCLUDE(FirstName, LastName);";
        private const string Index3 = "CREATE INDEX IX_Order_Deadline_Status ON [dbo].[Order](Deadline, Status) INCLUDE(Amount, ProductId) WHERE Status IN('Recieved', 'InProgress');";
        private const string Index4 = "CREATE INDEX IX_Payment_Deadline_Status ON [dbo].[Payment] (Deadline, Status) INCLUDE(Total) WHERE Status<> 'Rejected';";
        private const string Index5 = "CREATE UNIQUE INDEX IX_User_Username ON [dbo].[User](Username) INCLUDE(Email);";
        private const string Index6 = "CREATE UNIQUE INDEX IX_User_Email ON [dbo].[User](Email);";
        private const string Index7 = "CREATE UNIQUE INDEX IX_Customer_Email ON [dbo].[Customer](Email);";
        private const string Index8 = "CREATE UNIQUE INDEX IX_Employee_Email ON [dbo].[Employee](Email);";

        private const string DropIndex1 = "DROP INDEX IX_Employee_Email;";
        private const string DropIndex2 = "DROP INDEX UX_Customer_Email;";
        private const string DropIndex3 = "DROP INDEX IX_Order_Deadline_Status;";
        private const string DropIndex4 = "DROP INDEX IX_Payment_Deadline_Status;";
        private const string DropIndex5 = "DROP INDEX IX_User_Username;";
        private const string DropIndex6 = "DROP INDEX IX_User_Email;";
        private const string DropIndex7 = "DROP INDEX IX_Customer_Email;";
        private const string DropIndex8 = "DROP INDEX IX_Employee_Email;";

        //Up method is performed when the database is updated (upgrade)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{Index1}{Index2}{Index3}{Index4}{Index5}{Index6}{Index7}{Index8}");
        }

        //Down method is used to downgrade the migrations
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"{DropIndex1}{DropIndex2}{DropIndex3}{DropIndex4}{DropIndex5}{DropIndex6}{DropIndex7}{DropIndex8}");
        }
    }

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

//Finally we can write such method in API

#region API method to multi search

/*

public IQueryable<OfferingSummary> MultiWordSearchSql(params string[] filter)
{
    //To Table-Valued Parameter in c#. The solution is "DataTable" class
    var filterTable = new DataTable();
    filterTable.Columns.Add("Filter", typeof(string));
    foreach (var f in filter)
    {
        filterTable.Rows.Add($"%{f}%");
    }

    return context.FilteredOffers.FromSqlRaw(@"
        WITH Hits AS (
            SELECT  o.Id,
                    CASE WHEN
                        o.Title LIKE f.Filter
                        OR o.[Description] LIKE f.Filter
                        OR EXISTS (
                            SELECT  1
                            FROM    OfferingTags ot
                                    INNER JOIN OfferingsTags ots ON ot.Id = ots.TagsId
                            WHERE   ots.OfferingsId = o.Id
                                    AND ot.Tag LIKE f.Filter
                        )
                        THEN 1
                        ELSE 0
                    END AS ContainsFilter
            FROM    Offerings o
                    CROSS JOIN @Filters f
        ),
        FilteredHits AS (
            SELECT  h.Id,
                    SUM(h.ContainsFilter) AS Hits
            FROM    Hits h
            GROUP BY h.Id
            HAVING  SUM(h.ContainsFilter) > 0
        )
        SELECT  o.Id,
                o.Title,
                o.[Description],
                (
                        SELECT  STRING_AGG(ot.Tag, ', ')
                        FROM    OfferingTags ot
                                INNER JOIN OfferingsTags ots ON ot.Id = ots.TagsId
                        WHERE   ots.OfferingsId = o.Id
                ) AS Tags
        FROM    Offerings o
                INNER JOIN FilteredHits fh ON o.Id = fh.Id
        ORDER BY fh.Hits DESC",
        new SqlParameter("@Filters", filterTable) //TVP here -> Tabled-Valued Parameter
        {
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.FilterTags",
        }).AsNoTracking();
}

*/

#endregion API method to multi search