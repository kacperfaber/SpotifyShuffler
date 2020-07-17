﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SpotifyShuffler.Database
{
    public class User : IdentityUser<Guid>
    {
        [ForeignKey("SpotifyAccountId")]
        public SpotifyAccount SpotifyAccount { get; set; }

        public string SpotifyAccountId { get; set; }

        [InverseProperty("User")]
        public List<Operation> Operations { get; set; }
    }
}