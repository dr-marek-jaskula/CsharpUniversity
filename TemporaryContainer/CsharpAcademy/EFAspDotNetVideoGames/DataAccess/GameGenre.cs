using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EFAspDotNetVideoGames
{
    public class GameGenre
    {
        public int ID { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [JsonIgnore] //JsonIgnore from System.Text.Json (thats IMPORTANT!) if prevents from infinite cycle. This breaks the cycle
        public List<Game> Games { get; set; } 
        //then we can inert the game and the genre in one run (from insert game from postman)
    }

    public class Game
    {
        public int ID { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;

        public GameGenre Genre { get; set; }
        public int PersonalRating { get; set; }
    }
}
