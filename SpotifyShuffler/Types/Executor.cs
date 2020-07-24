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
        public SpotifyService SpotifyService;

        public ISpotifyPlaylistCreator PlaylistCreator;
        public ITracksAdder TracksAdder;

        public Executor(ISpotifyPlaylistCreator playlistCreator, ITracksAdder tracksAdder, SpotifyService spotifyService)
        {
            PlaylistCreator = playlistCreator;
            TracksAdder = tracksAdder;
            SpotifyService = spotifyService;
        }

        public async Task<ExecuteResult> ExecuteAsync(Operation operation, PlaylistPrototype playlistPrototype, User user, SpotifyAuthorization authorization)
        {
            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(authorization);

            if (operation.Kind == OperationKind.CreateNewPlaylist)
            {
                SpotifyPlaylist playlist = await PlaylistCreator.CreateAsync(operation, user, playlistService);

                await TracksAdder.AddAll(playlistPrototype, playlist, playlistService);

                return new ExecuteResult
                {
                    Playlist = playlist,
                    Operation = operation
                };
            }

            else if (operation.Kind == OperationKind.UseOriginalPlaylist)
            {
                SpotifyPlaylist playlist = await PlaylistCreator.CreateAsync(operation, user, playlistService);

                await TracksAdder.AddAll(playlistPrototype, playlist, playlistService);

                return new ExecuteResult
                {
                    Playlist = playlist,
                    Operation = operation
                };
            }
        }
    }
}