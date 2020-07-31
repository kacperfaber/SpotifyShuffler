using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class PlaylistDetailsController : Controller
    {
        public UserManager UserManager;
        public SpotifyService SpotifyService;
        public IAccessTokenStore AccessTokenStore;
        public OperationManager OperationManager;
        public ITrackModelsGenerator TrackModelsGenerator;

        public PlaylistDetailsController(OperationManager operationManager, IAccessTokenStore accessTokenStore, SpotifyService spotifyService,
            UserManager userManager, ITrackModelsGenerator trackModelsGenerator)
        {
            OperationManager = operationManager;
            AccessTokenStore = accessTokenStore;
            SpotifyService = spotifyService;
            UserManager = userManager;
            TrackModelsGenerator = trackModelsGenerator;
        }

        public async Task<IActionResult> Details(string playlistId)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(new SpotifyAuthorization
            {
                AccessToken = await AccessTokenStore.GetAccessToken(user)
            });
            SpotifyPlaylist spotifyPlaylist = await playlistService.GetPlaylist(playlistId);
            IEnumerable<Operation> operations = await OperationManager.GetAsync(playlistId);
            List<SpotifyTrack> tracks = await playlistService.GetAllTracks(playlistId, spotifyPlaylist.Tracks.Total);

            return View(new PlaylistDetailsModel
            {
                CurrentUser = user,
                Playlist = spotifyPlaylist,
                Occurrences = operations,
                Tracks = TrackModelsGenerator.Generate(tracks)
            });
        }
    }
}