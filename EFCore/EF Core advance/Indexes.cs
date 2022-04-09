namespace EFCore.EF_Core_advance;

public class Indexes
{
    //Here we will cover the topic of indexes for the example database (presented in this project)
    //It is better to create indexes when the database is populated with records. 
    //Therefore, the indexes will be applied to the database by Azure Data Studio and by MS SQL Server, not by c# Entity Framework Core.
    //Nevertheless, it is possible to create indexes in EF Core
    
    //One of the ways to do this is by Raw Sql with custom migrations -> See CustomMigrations.cs

    /* Transact-SQL (T-SQL) statement:
    CREATE [UNIQUE] INDEX index_name
    ON table_name(column_list)
    INCLUDE(included_column_list);
    WHERE predicate; 
    */

    //Indexes should be used to faster the query
    //However, they need to be used to faster the specific queries -> index designer needs to predict what queries will be used frequently and faster them by implementing indexes

    //    -- The additional indexes for CsharpUniversity

    //    --Naming convention for indexes:
    //    PK_ for primary keys
    //    UK_ for unique keys
    //    IX_ for non clustered non unique indexes
    //    UX_ for unique indexes

    //    Then the table name and the columns names

    //-- 1. NonClustered unique index for employee table on Email column with FirstName and LastName included and filtered for those employees that does not have email
    //CREATE UNIQUE INDEX UX_Employee_Email
    //ON [dbo].[Employee](Email)
    //INCLUDE(FirstName, LastName);

    //-- 2. NonClustered index: similar to the previous one
    //CREATE UNIQUE INDEX UX_Customer_Email
    //ON [dbo].[Customer](Email)
    //INCLUDE(FirstName, LastName);

    //-- 3. NonClustered nonunique index: for order table on Deadline and Status, Include Amount and ProductId, filter on 'Recieved' and 'InProgress' status
    //CREATE INDEX IX_Order_Deadline_Status
    //ON [dbo].[Order](Deadline, Status)
    //INCLUDE(Amount, ProductId)
    //WHERE Status IN ('Recieved', 'InProgress');

    //-- 4. NonClustered nonunique index: for payment table on Deadline and Status, Include Total, filter on non 'Rejected' status
    //CREATE INDEX IX_Payment_Deadline_Status
    //ON [dbo].[Payment](Deadline, Status)
    //INCLUDE(Total)
    //WHERE Status <> 'Rejected';
}

