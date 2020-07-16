using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PlaylistService
    {
        public Authorization Authorization { get; set; }

        public ITrackUriGenerator TrackUriGenerator;
        public SpotifyClient SpotifyClient;

        public Paging<SimpleSpotifyPlaylist> GetPlaylists(string spotifyId)
        {
            throw new NotImplementedException();
        }

        public SpotifyPlaylist GetPlaylist(string playlistId)
        {
            throw new NotImplementedException();
        }

        public async Task<SpotifyPlaylist> CreatePlaylist(string userId, string name, string description, bool @public, bool collaborative)
        {
            CreatePlaylistPayload payload = new CreatePlaylistPayload
            {
                Collaborative = collaborative,
                Description = description,
                Name = name,
                IsPublic = @public
            };

            return await SpotifyClient.SendAsync<SpotifyPlaylist>($"https://api.spotify.com/v1/users/{userId}/playlists", payload, HttpMethod.Post,
                Authorization);
        }

        public async Task AddTracks(string playlistId, params SimpleSpotifyTrack[] tracks)
        {
            AddPlaylistItemsPayload payload = new AddPlaylistItemsPayload
            {
                Position = 0,
                Uris = TrackUriGenerator.Generate(tracks).ToList()
            };

            await SpotifyClient.SendAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks", payload, HttpMethod.Post, Authorization);
        }

        public async Task<List<SimpleSpotifyTrack>> GetTracks(string playlistId)
        {
            throw new NotImplementedException();
        }
    }
}