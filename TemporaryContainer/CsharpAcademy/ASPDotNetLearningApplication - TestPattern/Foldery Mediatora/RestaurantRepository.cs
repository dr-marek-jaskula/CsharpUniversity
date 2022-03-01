using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Web;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASPDotNetLearningApplication
{
    public interface IRestaurantRepository
    {
        IEnumerable<RestaurantDto> GetAll();
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
    }

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDbContext _dbContex;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantRepository(RestaurantDbContext dbContex, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContex = dbContex;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContex.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes);

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDtos;
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var task = Task.Run(() =>
            {
                var restaurants = _dbContex.Restaurants
                     .Include(r => r.Address)
                     .Include(r => r.Dishes);
                var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
                return restaurantsDtos;
            });

            var result = await task;
            return result;
        }
    }
}
