using AutoMapper;
using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            //tutaj tworzymy mapowanie. Najpierw sk¹d dok¹d (w typach generycznych).
            //Potem jaki props (czyli member) w obiekcie na który mapujemy
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            //jeœli typy i nazwy w³aœciwoœci siê zgadzaj¹, to AutoMapper zmapuje automatycznie.

            CreateMap<Dish, DishDto>(); //te same s¹ wiêc zmapuje sam

            //ten profil mappowania robi z trzech propsów obiekt, gdzie te propsy nale¿¹
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address() { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
        }

    }
}
