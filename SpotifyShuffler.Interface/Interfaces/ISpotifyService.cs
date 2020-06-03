namespace SpotifyShuffler.Interface.Interfaces
{
    public interface ISpotifyService
    {
        PrivateSpotifyUser GetCurrentUser();

        PublicSpotifyUser GetUser(string id);

        Paging<SavedAlbum> GetUserSavedAlbums();

        Paging<SavedTrack> GetUserSavedTracks();

        Paging<SimpleSpotifyPlaylist> GetUserSavedPlaylists();
        
        
    }
}