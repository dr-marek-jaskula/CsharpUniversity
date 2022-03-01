using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFAspDotNetVideoGames
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly VideoGameDataContext _dbContext;

        public GamesController(VideoGameDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Game> GetAllGames()
        {
            return _dbContext.Games;
        }

        [HttpGet]
        [Route("{id}")]
        public Game GetGameByID([FromRoute] int id)
        {
            return _dbContext.Games.FirstOrDefault(g => g.ID == id);
        }

        [HttpPost]
        public async Task<Game> AddGame([FromBody] Game newGame)
        {
            _dbContext.Add(newGame);
            await _dbContext.SaveChangesAsync();
            return newGame;
        }
    }
}
