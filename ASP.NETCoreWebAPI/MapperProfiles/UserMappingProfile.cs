using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using AutoMapper;
using EFCore.Data_models;

namespace ASP.NETCoreWebAPI.MapperProfiles;

//If types and names are equal, AutoMapper will automate the process

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        //Map from RegisterUserDto to User
        CreateMap<RegisterUserDto, User>();

        //Map from Order to OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(o => o.Status, c => c.MapFrom(o => o.Status.ToString()));

        CreateMap<Payment, PaymentDto>()
            .ForMember(p => p.Status, c => c.MapFrom(o => o.Status.ToString()));

        CreateMap<Product, OrderProductDto>();

        CreateMap<Tag, TagDto>()
            .ForMember(t => t.ProductTag, c => c.MapFrom(t => t.ProductTag.ToString()));

        CreateMap<Address, AddressDto>();
        CreateMap<CreateAddressDto, Address>();
    }
}