using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database
{
    public class User : SimpleUser
    {
        [ForeignKey("SpotifyAccountId")]
        public SpotifyAccount SpotifyAccount { get; set; }

        public string SpotifyAccountId { get; set; }

        [InverseProperty("Owner")]
        public List<CompletedPlaylist> CompletedPlaylists { get; set; }
    }
}