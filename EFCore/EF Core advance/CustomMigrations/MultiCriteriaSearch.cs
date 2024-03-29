﻿namespace EFCore.EF_Core_advance.CustomMigrations;

public class MultiCriteriaSearch
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

    -- The first query (Hits) gives 0 or 1 for each filter.
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

    //To obtain Table-Valued Parameter in c# solution is "DataTable" class
    //Example:
    //var filterTable = new DataTable();
    //filterTable.Columns.Add("Filter", typeof(string));
}

//Finally we can write such method in API

#region API method to multi search

/*

public IQueryable<OfferingSummary> MultiWordSearchSql(params string[] filter)
{
    //To obtain Table-Valued Parameter in c# solution is "DataTable" class
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