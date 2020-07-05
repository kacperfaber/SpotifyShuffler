using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SpotifyShuffler.Database.Models
{
    public class User : IdentityUser<string>
    {
        [InverseProperty("Owner")]
        public List<PlaylistPrototype> PlaylistPrototypes { get; set; }

        [ForeignKey("EmailAddressId")]
        public EmailAddress EmailAddress { get; set; }

        public Guid EmailAddressId { get; set; }
    }
}