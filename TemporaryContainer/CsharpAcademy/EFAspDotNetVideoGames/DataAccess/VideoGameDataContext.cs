using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFAspDotNetVideoGames
{
    public class VideoGameDataContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> Genres { get; set; }

        public VideoGameDataContext(DbContextOptions<VideoGameDataContext> options) : base(options)
        {
        }
    }
}
