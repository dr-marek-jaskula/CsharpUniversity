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

namespace ASPDotNetLearningApplication
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);
        PageResult<RestaurantDto> GetAll(RestaurantQuery query);
        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContex;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext dbContex, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContex = dbContex;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContex.Restaurants.Include(r => r.Address).Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            //wszystkie elementy po filtrze
            var baseQuery = _dbContex.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                //tworzymy pomocniczy dictionary, który ma dostêpne trzy rodzaje sortowania
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description },
                    { nameof(Restaurant.Category), r => r.Category },
                };

                //patrzymy jaki selektor zostaw³ wybrany. Kolejnoœæ jest tutaj wa¿na, tzn przed baseQuery =
                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Ascending 
                    ? baseQuery.OrderBy(selectedColumn) 
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            //to include jest z EF Core, zeby poprzez Foregin Keys do³¹czyæ kolejne tabele do wyniku zapytania
            //dodajemy filtrowanie przez where. Trzeba uwzglêdniæ ró¿nicê duzych i ma³ych liter (dlatego robimy tam ToLower()
            var restaurants = baseQuery
                .Skip(query.PageSize * (query.PageNumber -1)) 
                .Take(query.PageSize)
                .ToList();
            //powyzszy skip pomija odpowiednia ilosc
            //take bierze odpowiednia ilosc od pocz¹tku

            //trzeba obliczyc jeszcze ile jest wszystkich elementow w bazie danych
            int totalItemsCount = baseQuery.Count();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            var result = new PageResult<RestaurantDto>(restaurantsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);

            //dodajemy to do autoryzacji po zasobach
            restaurant.CreatedById = _userContextService.GetUserId;

            //teraz dodajemy do bazy przez EF
            _dbContex.Restaurants.Add(restaurant);
            //teraz zapisujemy zmiany
            _dbContex.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(int id)
        {
            //loggowanie wpisów, chyba w takich nie powinno sie robi, ale moze sie powinno
            //_logger.LogWarning($"Restaurant with id: {id} Delete action invoked");
            //_logger.LogError($"Error! Restaurant with id: {id} Delete action invoked");

            var restaurant = _dbContex.Restaurants.FirstOrDefault(r => r.Id == id);

            //false oznacza ze nie usuniêto bo nie ma takiego
            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded) throw new ForbidException();

            _dbContex.Restaurants.Remove(restaurant);
            _dbContex.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContex.Restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");

            //dodajemy autoryzacjê i zapisujemy w zmiennej tê wiedzê
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            //jesli nie ma autoryzacji to wywalamy wyj¹tek (nowy, wiêc trzeba go obs³u¿yæ)
            if (!authorizationResult.Succeeded) throw new ForbidException();

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContex.SaveChanges();
        }
    }
}
