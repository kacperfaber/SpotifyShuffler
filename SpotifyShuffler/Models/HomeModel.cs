namespace SpotifyShuffler.Models
{
    public class HomeModel : LayoutModel
    {
        public PlaylistModel[] Playlists { get; set; }
        public int TotalPlaylists { get; set; }
    }
}