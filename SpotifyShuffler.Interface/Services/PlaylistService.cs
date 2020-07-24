﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyShuffler.Interface
{
    public class PlaylistService : ServiceBase
    {
        public ITrackUriGenerator TrackUriGenerator;
        public SpotifyClient SpotifyClient;
        public IUriFilter UriFilter;

        public PlaylistService(SpotifyClient spotifyClient, ITrackUriGenerator trackUriGenerator, IUriFilter uriFilter)
        {
            SpotifyClient = spotifyClient;
            TrackUriGenerator = trackUriGenerator;
            UriFilter = uriFilter;
        }

        public async Task<Paging<SimpleSpotifyPlaylist>> GetPlaylists(int limit = 20, int offset = 0)
        {
            const string url = "https://api.spotify.com/v1/me/playlists";

            object queryParameters = new
            {
                Limit = limit,
                Offset = offset
            };

            return await SpotifyClient.SendAsync<Paging<SimpleSpotifyPlaylist>>(url, queryParameters, null, HttpMethod.Get, SpotifyAuthorization);
        }

        public async Task<SpotifyPlaylist> GetPlaylist(string playlistId)
        {
            string url = $"https://api.spotify.com/v1/playlists/{playlistId}";

            object query = new
            {
                Fields = "fields=tracks.items(track(name,href,album(name,href)))"
            };

            return await SpotifyClient.SendAsync<SpotifyPlaylist>(url, query, null, HttpMethod.Get, SpotifyAuthorization);
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
                SpotifyAuthorization);
        }

        public async Task AddTracks(string playlistId, IEnumerable<string> uris)
        {
            AddPlaylistItemsPayload payload = new AddPlaylistItemsPayload
            {
                Position = 0,
                Uris = uris.ToList()
            };

            HttpResponseMessage response = await SpotifyClient.SendAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks", payload, HttpMethod.Post,
                SpotifyAuthorization);
        }

        public async Task AddAllTracks(string playlistId, IEnumerable<string> uris)
        {
            IEnumerable<string> filteredUris = UriFilter.Filter(uris);

            int count = filteredUris.Count();
            int totalLoops = count / 100;
            int left = count % 100;

            for (int i = 0; i < totalLoops; i++)
            {
                await AddTracks(playlistId, filteredUris.Skip(100 * i).Take(100));
            }

            if (left > 0)
                await AddTracks(playlistId, filteredUris.TakeLast(left));
        }

        public async Task AddTracks(string playlistId, params SimpleSpotifyTrack[] tracks)
        {
            AddPlaylistItemsPayload payload = new AddPlaylistItemsPayload
            {
                Position = 0,
                Uris = TrackUriGenerator.Generate(tracks).ToList()
            };

            await SpotifyClient.SendAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks", payload, HttpMethod.Post, SpotifyAuthorization);
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

            return await SpotifyClient.SendAsync<Paging<PlaylistTrackObject>>(url, query, null, HttpMethod.Get, SpotifyAuthorization);
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

        public async Task Clear(string playlistId, IEnumerable<string> uris)
        {
            List<SpotifyUri> spotifyUris = Array.ConvertAll(uris.ToArray(), s => new SpotifyUri {ItemUri = s}).ToList();

            DeletePlaylistTracksPayload payload = new DeletePlaylistTracksPayload
            {
                Tracks = spotifyUris
            };

            string url = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";

            HttpResponseMessage response = await SpotifyClient.SendAsync(url, payload, HttpMethod.Delete, SpotifyAuthorization);
        }

        public async Task ClearAll(SpotifyPlaylist playlist)
        {
            List<SpotifyTrack> tracks = await GetAllTracks(playlist.Id, playlist.Tracks.Total);

            List<string> spotifyUris = Array.ConvertAll(tracks.ToArray(), s => s.Uri)
                .Distinct()
                .ToList();

            int count = spotifyUris.Count;
            int loops = count / 100;
            int left = count % 100;

            for (int i = 0; i < loops; i++)
            {
                IEnumerable<string> uris = spotifyUris
                    .Skip(100 * i)
                    .Take(100);

                await Clear(playlist.Id, uris);
            }

            IEnumerable<string> leftUris = spotifyUris
                .TakeLast(left);

            await Clear(playlist.Id, leftUris);
        }
    }
}