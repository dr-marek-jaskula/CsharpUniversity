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
    }
}

//public class RestaurantMappingProfile : Profile
//{
//    public RestaurantMappingProfile()
//    {
//        //tutaj tworzymy mapowanie. Najpierw sk¹d dok¹d (w typach generycznych).
//        //Potem jaki props (czyli member) w obiekcie na który mapujemy
//        CreateMap<Restaurant, RestaurantDto>()
//            .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
//            .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
//            .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

//        //ten profil mappowania robi z trzech propsów obiekt, gdzie te propsy nale¿¹
//        CreateMap<CreateRestaurantDto, Restaurant>()
//            .ForMember(r => r.Address, c => c.MapFrom(dto => new Address() { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
//    }
//}