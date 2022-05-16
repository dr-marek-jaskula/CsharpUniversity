namespace EFCore.EF_Core_basics;

public class OnDeleteClient
{
    //In order to obtain the DeleteCascade not in the database but in the memory (what will be reflected in the database) we can use:
    //.OnDelete(DeleteBehavior.ClientCascade);
    //In the model configuration part

    //This approach will not result in any changes to the database.
    //Therefore, no migrations need to be applied

    //For entities being tracked by the DbContext, dependent entities will be deleted when the related principal is deleted.

    //We can also use "ClientSetNull" or "ClientNoAction"
}