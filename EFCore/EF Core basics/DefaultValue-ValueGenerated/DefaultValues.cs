namespace EFCore.EF_Core_basics.DefaultValue;

public class DefaultValues
{
    //We can choose two option for inserting the default values in the Entity Framework Core

    //1. Use "HasDefaultValue" approach like:
    /*
        builder.Property(o => o.Status)
            .IsRequired(true)
            .HasColumnType("VARCHAR(10)")
            .HasDefaultValue(Status.Received)
            .HasConversion(status => status.ToString(),
             s => (Status)Enum.Parse(typeof(Status), s))
            .HasComment("Received, InProgress, Done or Rejected");
    */

    //Bad example: this will create a DateTime that is equal to the migration DateTime creation!!
    /*
        builder.Property(u => u.CreateTime)
            .HasDefaultValue(DateTime.Now.Date)
            .HasColumnType("DATE");
    */

    //2. Use "HasDefaultValueSql" approach like:

    /*
        builder.Property(u => u.CreatedDate)
            .HasDefaultValueSql("getutcdate()") //need to use HasDefaultValueSql with "getutcdate" because it need to be the sql command
            .HasColumnType("DATETIME2");
    */
}