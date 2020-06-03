using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Database.Contexts
{
    public class SpotifyContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options)
        {
        }
    }
}