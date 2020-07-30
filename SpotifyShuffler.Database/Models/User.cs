using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SpotifyShuffler.Database
{
    public class User : SimpleUser
    {
        [ForeignKey("SpotifyAccountId")]
        public SpotifyAccount SpotifyAccount { get; set; }

        public string SpotifyAccountId { get; set; }

        [InverseProperty("Owner")]
        public List<CompletedPlaylist> CompletedPlaylists { get; set; }

        [InverseProperty("User")]
        public List<EmailAddress> EmailAddresses { get; set; }
        
        public bool IsDeleted { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}