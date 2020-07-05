using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpotifyShuffler.Database
{
    public class SpotifyContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistPrototype> PlaylistPrototypes { get; set; }
        public DbSet<PlaylistPrototypeData> PlaylistPrototypeDatas { get; set; }
        public DbSet<SpotifyAccount> SpotifyAccounts { get; set; }
        
        public SpotifyContext(DbContextOptions options) : base(options)
        {
        }

        public SpotifyContext()
        {
        }
    }
}