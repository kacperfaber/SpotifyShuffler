using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Database.Contexts
{
    public class SpotifyContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistPrototype> PlaylistPrototypes { get; set; }
        public DbSet<PlaylistPrototypeData> PlaylistPrototypeDatas { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        
        public SpotifyContext(DbContextOptions options) : base(options)
        {
        }

        public SpotifyContext()
        {
        }
    }
}