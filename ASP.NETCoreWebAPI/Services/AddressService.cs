using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Logging;
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
    private readonly ILoggerAdapter<IOrderService> _logger;

    public AddressService(MyDbContext dbContex, IMapper mapper, ILoggerAdapter<IOrderService> logger)
    {
        _dbContex = dbContex;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Create(CreateAddressDto dto)
    {
        var address = _mapper.Map<Address>(dto);
        _dbContex.Addresses.Add(address);
        await _dbContex.SaveChangesAsync();
    }

    public AddressDto GetByIdSimpleMappingLowerPerformance(int id)
    {
        var address = _dbContex.Addresses
            .FirstOrDefault(a => a.Id == id);

        if (address is null)
            throw new NotFoundException("Address not found");

        var result = _mapper.Map<AddressDto>(address);
        return result;
    }
    //Resulting query
    /*
    SELECT TOP(1) [a].[Id], [a].[Building], [a].[City], [a].[Country], [a].[Flat], [a].[Street], [a].[ZipCode], [a].[Latitude], [a].[Longitude]
    FROM [Address] AS [a]
    WHERE [a].[Id] = @__id_0
     */
    //performance is worse because we query the whole record and then map

    public AddressDto GetById(int id)
    {
        var address = _dbContex.Addresses.Where(p => p.Id == id);

        var result = _mapper.ProjectTo<AddressDto>(address).FirstOrDefault(); //We could use "First" here but the exception would not be costume one (with message "Sequence contains no elements")

        if (result is null)
            throw new NotFoundException("Address not found");

        return result;
    }
    //Resulting query 
    /*
    SELECT TOP(1) [a].[Id], [a].[City], [a].[Country], [a].[ZipCode], [a].[Street], COALESCE([a].[Building], CAST(0 AS TINYINT)), COALESCE([a].[Flat], CAST(0 AS TINYINT))
    FROM [Address] AS [a]
    WHERE [a].[Id] = @__id_0
     */
    //Performance is better because we just query what we need and not the whole record and then map
}