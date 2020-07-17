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

        public PlaylistService(SpotifyClient spotifyClient, ITrackUriGenerator trackUriGenerator)
        {
            SpotifyClient = spotifyClient;
            TrackUriGenerator = trackUriGenerator;
        }

        public async Task<Paging<SimpleSpotifyPlaylist>> GetPlaylists(int limit = 20, int offset = 0)
        {
            string url = "https://api.spotify.com/v1/me/playlists";

            object queryParameters = new {Limit = limit, Offset = offset};

            return await SpotifyClient.SendAsync<Paging<SimpleSpotifyPlaylist>>(url, queryParameters, new object(), HttpMethod.Get, Authorization);
        }

        public async Task<SpotifyPlaylist> GetPlaylist(string playlistId)
        {
            string url = $"https://api.spotify.com/v1/playlists/{playlistId}";

            object query = new
            {
                Fields = "fields=tracks.items(track(name,href,album(name,href)))"
            };
            
            return await SpotifyClient.SendAsync<SpotifyPlaylist>(url, query, new object(), HttpMethod.Get, Authorization);
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

        public async Task<List<SimpleSpotifyTrack>> GetTracks(string playlistId, int limit = 100)
        {
            object query = new
            {
                Fields = "fields=items(track(name,href,album(name,href)))",
                Limit = limit
            };

            string url = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";

            return await SpotifyClient.SendAsync<List<SimpleSpotifyTrack>>(url, query, new object(), HttpMethod.Get, Authorization);
        }
    }
}