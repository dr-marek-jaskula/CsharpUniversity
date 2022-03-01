using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASPDotNetLearningApplication
{

    //klasa musi implementowaæ interfejs IRequestHandler<Tin, Tout>, gdzie w Tin musimy podaæ klasê obs³ugiwanego request-u. Tout to zwracany, przez metodê Handle, typ danych

    public class GetAllRestaurantsHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public GetAllRestaurantsHandler(IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurantsDtos = await _restaurantRepository.GetAllAsync();
            return restaurantsDtos;
        }
    }
}
