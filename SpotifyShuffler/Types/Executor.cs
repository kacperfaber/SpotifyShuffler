using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Types
{
    public class Executor
    {
        public ISpotifyPlaylistCreator PlaylistCreator;
        public SpotifyService SpotifyService;
        public ITracksAdder TracksAdder;
        public ISpotifyTracksShuffler Shuffler;

        public Executor(ISpotifyPlaylistCreator playlistCreator, ITracksAdder tracksAdder, SpotifyService spotifyService, ISpotifyTracksShuffler shuffler)
        {
            PlaylistCreator = playlistCreator;
            TracksAdder = tracksAdder;
            SpotifyService = spotifyService;
            Shuffler = shuffler;
        }

        public async Task<ExecuteResult> ExecuteAsync(Operation operation, User user, SpotifyAuthorization authorization)
        {
            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(authorization);
            SpotifyPlaylist originalPlaylist = await playlistService.GetPlaylist(operation.OriginalPlaylistId);

            if (operation.Kind == OperationKind.CreateNewPlaylist)
            {
                List<SpotifyTrack> tracks = await playlistService.GetAllTracks(operation.OriginalPlaylistId, originalPlaylist.Tracks.Total);
                IOrderedEnumerable<SpotifyTrack> shuffledTracks = await Shuffler.ShuffleAsync(tracks);

                SpotifyPlaylist playlist = await PlaylistCreator.CreateAsync(operation, user, playlistService);
                
                await TracksAdder.AddAll(shuffledTracks, playlist, playlistService);

                return new ExecuteResult
                {
                    Playlist = playlist,
                    Operation = operation
                };
            }

            else if (operation.Kind == OperationKind.UseOriginalPlaylist)
            {
                List<SpotifyTrack> tracks = await playlistService.GetAllTracks(operation.OriginalPlaylistId, originalPlaylist.Tracks.Total);

                await playlistService.Clear(originalPlaylist);
                
                IOrderedEnumerable<SpotifyTrack> shuffledTracks = await Shuffler.ShuffleAsync(tracks);
                await TracksAdder.AddAll(shuffledTracks, originalPlaylist, playlistService);

                return new ExecuteResult
                {
                    Playlist = originalPlaylist,
                    Operation = operation
                };
            }

            throw new Exception();
        }
    }
}