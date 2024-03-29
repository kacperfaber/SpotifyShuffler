﻿using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpotifyShuffler.Database
{
    public class SpotifyContext : IdentityDbContext<User, Role, Guid>
    {
        public SpotifyContext(DbContextOptions options) : base(options)
        {
        }

        public SpotifyContext()
        {
        }

        public DbSet<SpotifyAccount> SpotifyAccounts { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<CompletedPlaylist> CompletedPlaylists { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<ConfirmationCode> ConfirmationCodes { get; set; }
    }
}