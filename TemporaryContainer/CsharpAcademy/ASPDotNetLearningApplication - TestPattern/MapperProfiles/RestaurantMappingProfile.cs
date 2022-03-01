using AutoMapper;
using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            //tutaj tworzymy mapowanie. Najpierw sk�d dok�d (w typach generycznych).
            //Potem jaki props (czyli member) w obiekcie na kt�ry mapujemy
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            //je�li typy i nazwy w�a�ciwo�ci si� zgadzaj�, to AutoMapper zmapuje automatycznie.

            CreateMap<Dish, DishDto>(); //te same s� wi�c zmapuje sam

            //ten profil mappowania robi z trzech props�w obiekt, gdzie te propsy nale��
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address() { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
        }

    }
}
