using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using AutoMapper;
using EFCore;
using EFCore.Data_models;

namespace ASP.NETCoreWebAPI.Services;

public interface IAddressService
{
    AddressDto GetById(int id);

    Task Create(CreateAddressDto dto);
}

public class AddressService : IAddressService
{
    private readonly MyDbContext _dbContex;
    private readonly IMapper _mapper;

    public AddressService(MyDbContext dbContex, IMapper mapper)
    {
        _dbContex = dbContex;
        _mapper = mapper;
    }

    public async Task Create(CreateAddressDto dto)
    {
        var address = _mapper.Map<Address>(dto);
        _dbContex.Addresses.Add(address);
        await _dbContex.SaveChangesAsync();
    }

    public AddressDto GetById(int id)
    {
        var address = _dbContex.Addresses
            .FirstOrDefault(a => a.Id == id);

        if (address is null)
            throw new NotFoundException("Address not found");

        var result = _mapper.Map<AddressDto>(address);
        return result;
    }

    //TODO : measure what is faster - benchmarks
    public AddressDto GetById2(int id)
    {
        var address = _dbContex.Addresses.Where(p => p.Id == id);

        var result = _mapper.ProjectTo<AddressDto>(address).FirstOrDefault();

        if (result is null)
            throw new NotFoundException("Address not found");

        return result;
    }
}