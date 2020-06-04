using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Database.Contexts
{
    public class SpotifyContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }

        public DbSet<PrimaryArtist> PrimaryArtists { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SpotifyUser> SpotifyUsers { get; set; }

        public DbSet<PlaylistPrototype> PlaylistPrototypes { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<Authorization> Authorizations { get; set; }

        public DbSet<ApiRequest> ApiRequests { get; set; }

        public DbSet<ApiResponse> ApiResponses { get; set; }

        public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options)
        {
        }
    }
}