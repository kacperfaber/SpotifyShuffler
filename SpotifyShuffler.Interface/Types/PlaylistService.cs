﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

            object queryParameters = new
            {
                Limit = limit,
                Offset = offset
            };

            return await SpotifyClient.SendAsync<Paging<SimpleSpotifyPlaylist>>(url, queryParameters, null, HttpMethod.Get, Authorization);
        }

        public async Task<SpotifyPlaylist> GetPlaylist(string playlistId)
        {
            string url = $"https://api.spotify.com/v1/playlists/{playlistId}";

            object query = new
            {
                Fields = "fields=tracks.items(track(name,href,album(name,href)))"
            };

            return await SpotifyClient.SendAsync<SpotifyPlaylist>(url, query, null, HttpMethod.Get, Authorization);
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

        public async Task<Paging<PlaylistTrackObject>> GetTracks(string playlistId, int limit = 100, int offset = 0)
        {
            object query = new
            {
                Fields = "fields=items(track(name,href,album(name,href)))",
                Limit = limit,
                Offset = offset
            };

            string url = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";

            return await SpotifyClient.SendAsync<Paging<PlaylistTrackObject>>(url, query, null, HttpMethod.Get, Authorization);
        }

        public async Task<List<SpotifyTrack>> GetAllTracks(string playlistId, int total)
        {
            int fullLoops = total / 100;
            int left = total % 100;
            
            List<SpotifyTrack> tracks = new List<SpotifyTrack>(total);

            for (int i = 0; i < fullLoops; i++)
            {
                int offset = i * 100;
                const int limit = 100;

                PlaylistTrackObject[] items = (await GetTracks(playlistId, limit, offset)).Items;
                
                tracks.AddRange(Array.ConvertAll(items, x => x.Track));
            }
            
            PlaylistTrackObject[] leftItems = (await GetTracks(playlistId, left, total / 100)).Items;
                
            tracks.AddRange(Array.ConvertAll(leftItems, x => x.Track));

            return tracks;
        }
    }
}