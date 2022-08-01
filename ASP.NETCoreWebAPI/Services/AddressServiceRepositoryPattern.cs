using AutoMapper;
using EFCore.Data_models;
using ASP.NETCoreWebAPI.Repositories;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;

namespace ASP.NETCoreWebAPI.Services;

public class AddressServiceRepositoryPattern : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressServiceRepositoryPattern(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task Create(CreateAddressDto dto)
    {
        var address = _mapper.Map<Address>(dto);
        
        await _addressRepository.Create(address);
    }

    public AddressDto GetById(int id)
    {
        var address = _addressRepository.GetById(id);

        var result = _mapper.Map<AddressDto>(address);

        return result;
    }
}