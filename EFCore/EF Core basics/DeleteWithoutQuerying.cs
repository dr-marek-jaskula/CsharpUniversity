namespace EFCore.EF_Core_basics;

public class DeleteWithoutQuerying
{
    //In some cases, when we want to delete a record from a database with certain id, we could do this without querying it.
    //This will improve the performance of the application.

    //Delete a record without querying it
    //public void Delete(int id)
    //{
    //    //At first we create a new Order with an id given by the user
    //    var orderToDelete = new Order() { Id = id };
    //    //Then we Attach this instance to the ChangeTracker and get the entry from it
    //    var entry = _dbContex.Orders.Attach(orderToDelete);
    //    //Next we change the entry status to "Delete"
    //    entry.State = EntityState.Deleted;
    //    //Finally we save changes
    //    try
    //    {
    //        _dbContex.SaveChanges();
    //    }
    //    catch
    //    {
    //        throw new NotFoundException($"Order with id = {id} not found");
    //    }
    //}
}