using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Payloads;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize]
    public class OperationController : Controller
    {
        public UserManager UserManager;
        public OperationManager OperationManager;
        public IAccessTokenStore AccessTokenStore;
        public SpotifyService SpotifyService;
        public IPlaylistPrototypeGenerator PlaylistPrototypeGenerator;
        public SpotifyContext SpotifyContext;
        public IModelIndexer ModelIndexer;

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore,
            SpotifyService spotifyService, IPlaylistPrototypeGenerator playlistPrototypeGenerator, SpotifyContext spotifyContext, IModelIndexer modelIndexer)
        {
            OperationManager = operationManager;
            UserManager = userManager;
            AccessTokenStore = accessTokenStore;
            SpotifyService = spotifyService;
            PlaylistPrototypeGenerator = playlistPrototypeGenerator;
            SpotifyContext = spotifyContext;
            ModelIndexer = modelIndexer;
        }

        [HttpGet("operation/begin-new")]
        public async Task<IActionResult> BeginNewOperation(string playlistId)
        {
            User user = UserManager.GetUserAsync(HttpContext.User).Result;

            SpotifyAuthorization authorization = new SpotifyAuthorization {AccessToken = await AccessTokenStore.GetAccessToken(user)};

            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(authorization);

            SpotifyPlaylist playlist = await playlistService.GetPlaylist(playlistId);

            Operation operation = new Operation
            {
                CreatedAt = DateTime.Now,
                OriginalPlaylistId = playlistId,
                User = user
            };

            await OperationManager.CreateAsync(operation);

            return RedirectToAction("MakePrototype", new {operation_id = operation.Id, playlist_id = playlist.Id});
        }

        [HttpGet("operation/make-prototype")]
        public async Task<IActionResult> MakePrototype(MakePrototypePayload payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);
            User user = await UserManager.GetUserAsync(HttpContext.User);

            PlaylistPrototype prototype = operation.Prototype;
            
            if (prototype == null)
            {
                SpotifyAuthorization auth = new SpotifyAuthorization
                {
                    AccessToken = await AccessTokenStore.GetAccessToken(user)
                };

                PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(auth);

                operation.Prototype = await PlaylistPrototypeGenerator.GenerateAsync(await playlistService.GetPlaylist(operation.OriginalPlaylistId), operation);
                
                SpotifyContext.Update(operation);
                _ =  SpotifyContext.SaveChangesAsync();

                return Json(operation.Prototype);
            }

            else
            {
                List<TrackPrototype> tracks = prototype.Tracks
                    .OrderBy(x => new Random().Next(0, 10000))
                    .ForEach(x => x.PlaylistPrototype = prototype)
                    .ToList();
                
                ModelIndexer.Index(tracks, x => x.Index);

                SpotifyContext.UpdateRange(tracks);
                _ = SpotifyContext.SaveChangesAsync();
                
                TrackPrototype track = tracks.FirstOrDefault();

                return Content($"Updating existing prototype.\n Now first position is {track.Author} - \"{track.Name}\"");
            }
        }

        [HttpGet("operation/name-your-playlist")]
        public async Task<IActionResult> NameYourPlaylist()
        {
            // Set name and description of new spotify playlist.

            throw new NotImplementedException();
        }

        [HttpPost("operation/name-your-playlist/post")]
        public async Task<IActionResult> NameYourPlaylistPost()
        {
            throw new NotImplementedException();
        }

        [HttpGet("operation/submit")]
        public async Task<IActionResult> SubmitOperation()
        {
            // Show all data we have about new playlist,
            // Author, Is public, name, description, original playlist and ask 'Do You Confirm?'

            throw new NotImplementedException();
        }

        [HttpPost("operation/execute")]
        public async Task<IActionResult> SubmitOperationPost()
        {
            // Job accepted.
            // CreatePlaylist and AddTracks.


            throw new NotImplementedException();
        }
    }
}