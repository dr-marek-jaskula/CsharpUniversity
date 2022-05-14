namespace EFCore.EF_Core_basics.DefaultValue_ValueGenerated;

public class ValueGenerated
{
    //1. ValueGeneratedOnAddOrUpdate approach: the default value will be applied when the change is done to the certain record (good for "UpdateDate")

    /*
        builder.Property(u => u.UpdatedDate)
            .ValueGeneratedOnAddOrUpdate() //Generate the value when the update is made and when data is added
            .HasDefaultValueSql("getutcdate()") //need to use HasDefaultValueSql with "getutcdate" because it need to be the sql command
            .HasColumnType("DATETIME2");
    */

    //We can also use other cases
}