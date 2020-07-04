using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler.Models
{
    public class LayoutModel
    {
        public string Title { get; set; } = "SpotifyShuffler";

        public User CurrentUser { get; set; }
    }
}