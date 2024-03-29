﻿using SpotifyShuffler.Database;

namespace SpotifyShuffler.Models
{
    public class SettingsModel : LayoutModel
    {
        public SpotifyAccount SpotifyAccount { get; set; }
        
        public EmailAddress EmailAddress { get; set; }
    }
}