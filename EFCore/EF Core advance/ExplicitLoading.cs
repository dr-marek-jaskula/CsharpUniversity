using System.Linq;

namespace EFCore.EF_Core_advance;

public class ExplicitLoading
{
    //Explicit Loading happens when we already retrieved the record from a database but then we want to retrieve also a related entity.
    private readonly MyDbContext _context;

    public ExplicitLoading(MyDbContext context)
    {
        _context = context;
    }

    public void InvokeExplicitLoadingExample()
    {
        //Lets consider the case when we at first get the following record
        var avon = _context.Shops
                    .Where(s => s.Name == "Avon")
                    .FirstOrDefault();

        if (avon is null)
            return;
        //some code

        //Then we want to retrieve a related data
        _context.Entry(avon).Reference(s => s.Address).Load(); // loads shop address
        //Then we want to retrieve a collection of related data
        _context.Entry(avon).Collection(s => s.Employees).Load(); // loads shop employees collection

        //It is also possible to create a more complicated query to retrieve related data:
        _context.Entry(avon)
           .Collection(s => s.Products)
           .Query()
               .Where(p => p.Price < 70)
               .FirstOrDefault(p => p.Name == "Clock");
    }
}