using System;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Types
{
    public class Executor
    {
        public SpotifyService SpotifyService { get; set; }

        public ISpotifyPlaylistCreator PlaylistCreator;
        public ITracksAdder TracksAdder;

        public Executor(ISpotifyPlaylistCreator playlistCreator, ITracksAdder tracksAdder)
        {
            PlaylistCreator = playlistCreator;
            TracksAdder = tracksAdder;
        }

        public async Task<ExecuteResult> ExecuteAsync(Operation operation, PlaylistPrototype playlistPrototype, User user, SpotifyAuthorization authorization)
        {
            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(authorization);

            SpotifyPlaylist playlist = await PlaylistCreator.CreateAsync(operation, user, playlistService);

            await TracksAdder.AddAll(playlistPrototype, playlist, playlistService);

            return new ExecuteResult
            {
                Playlist = playlist
            };
        }
    }
}