using System;

namespace SpotifyShuffler.Interface
{
    public class PlaylistService
    {
        public Paging<SimpleSpotifyPlaylist> GetPlaylists(string spotifyId)
        {
            throw new NotImplementedException();
        }

        public SpotifyPlaylist GetPlaylist(string playlistId)
        {
            throw new NotImplementedException();
        }

        public SpotifyPlaylist CreatePlaylist(string name, string description)
        {
            throw new NotImplementedException();
        }

        public void AddTracks(SpotifyPlaylist playlist, params SimpleSpotifyTrack[] tracks)
        {
            throw new NotImplementedException();
        }
    }
}