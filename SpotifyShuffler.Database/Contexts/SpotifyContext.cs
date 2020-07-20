using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpotifyShuffler.Database
{
    public class SpotifyContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<PlaylistPrototype> PlaylistPrototypes { get; set; }
        public DbSet<SpotifyAccount> SpotifyAccounts { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<CompletedPlaylist> CompletedPlaylists { get; set; }

        public SpotifyContext(DbContextOptions options) : base(options)
        {
        }

        public SpotifyContext()
        {
        }
    }
}