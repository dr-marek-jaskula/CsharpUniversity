using EFCore;
using ASP.NETCoreWebAPI.Exceptions;
using EFCore.Data_models;

namespace ASP.NETCoreWebAPI.Repositories;

public interface IAddressRepository
{
    Address GetById(int id);

    Task Create(Address order);
}

public class AddressRepository : IAddressRepository
{
    private readonly MyDbContext _dbContext;

    public AddressRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Address address)
    {
        _dbContext.Addresses.Add(address);

        await _dbContext.SaveChangesAsync();
    }

    public Address GetById(int id)
    {
        var address = _dbContext.Addresses
            .FirstOrDefault(a => a.Id == id);

        if (address is null)
            throw new NotFoundException("Address not found");

        return address;
    }
}
