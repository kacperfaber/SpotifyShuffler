﻿using SpotifyShuffler.Database;

namespace SpotifyShuffler.Models
{
    public class DeleteEmailAddressModel : LayoutModel
    {
        public EmailAddress EmailAddress { get; set; }
        
        public bool IsConfirmed { get; set; }
    }
}