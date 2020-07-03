using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Database.Contexts
{
    public class SpotifyContext : DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistPrototype> PlaylistPrototypes { get; set; }
        public DbSet<PlaylistPrototypeData> PlaylistPrototypeDatas { get; set; }
        public DbSet<User> Users { get; set; }
        
        public SpotifyContext(DbContextOptions options) : base(options)
        {
        }

        public SpotifyContext()
        {
        }
    }
}