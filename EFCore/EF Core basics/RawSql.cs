using Microsoft.EntityFrameworkCore;

namespace EFCore.EF_Core_basics;

public class RawSql
{
    private readonly MyDbContext _myDbContext;

    public RawSql(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    //In order to execute the raw sql on the database, using EF Core we use:
    public void ExampleRawSql()
    {
        //To use raw sql on database, independently from tables
        _myDbContext.Database.ExecuteSqlRaw(@"
        UPDATE Review
        SET UpdatedDate = GETDATE()
        WHERE Id = 4;
        ");

        //In order to use string interpolation in the RawSql and PROTECT the application from raw sql injection, we need to use:

        var valueToInterpolate = "85";

        var tags = _myDbContext.Tags
        .FromSqlInterpolated($@"
        SELECT Tag.Id, Tag.ProductTag
        FROM Tag
        WHERE Tag.Id > {valueToInterpolate};
        ")
        .ToList();
        //The interpolated string will be set as parameter
    }
}