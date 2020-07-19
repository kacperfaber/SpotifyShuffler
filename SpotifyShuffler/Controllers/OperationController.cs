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
using SpotifyShuffler.Models;
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

        public IPrototypesSorter PrototypesSorter;

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore,
            SpotifyService spotifyService, IPlaylistPrototypeGenerator playlistPrototypeGenerator, SpotifyContext spotifyContext,
            IPrototypesSorter prototypesSorter)
        {
            OperationManager = operationManager;
            UserManager = userManager;
            AccessTokenStore = accessTokenStore;
            SpotifyService = spotifyService;
            PlaylistPrototypeGenerator = playlistPrototypeGenerator;
            SpotifyContext = spotifyContext;
            PrototypesSorter = prototypesSorter;
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

                operation.Prototype =
                    await PlaylistPrototypeGenerator.GenerateAsync(await playlistService.GetPlaylist(operation.OriginalPlaylistId), operation);
                PrototypesSorter.Sort(operation.Prototype);

                operation.Prototype.Tracks.ForEach(x => x.PlaylistPrototype = operation.Prototype);

                SpotifyContext.Add(operation.Prototype);
                _ = SpotifyContext.SaveChangesAsync();

                return Json(operation.Prototype);
            }

            else
            {
                PrototypesSorter.Sort(prototype);

                SpotifyContext.Update(prototype);
                _ = SpotifyContext.SaveChangesAsync();

                TrackPrototype track = prototype.Tracks.FirstOrDefault();

                return Content($"Updating existing prototype.\n Now first position is {track.Author} - \"{track.Name}\"");
            }
        }

        [HttpGet("operation/name-your-playlist")]
        public IActionResult NameYourPlaylist(NameYourPlaylistPayload payload)
        {
            NameYourPlaylist model = new NameYourPlaylist
            {
                PlaylistId = payload.PlaylistId,
                OperationId = payload.OperationId
            };
            
            return View(model);
        }

        [HttpPost("operation/name-your-playlist/post")]
        public async Task<IActionResult> NameYourPlaylistPost(NameYourPlaylist payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);

            operation.PlaylistName = payload.PlaylistName;
            operation.PlaylistDescription = payload.PlaylistDescription;

            await OperationManager.UpdateAsync(operation);

            return Content("accepted.");
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