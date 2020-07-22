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
        public IOperationValidator OperationValidator;
        public IPlaylistValidator PlaylistValidator;
        public Executor Executor;
        public ICompletedPlaylistGenerator CompletedPlaylistGenerator;

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore,
            SpotifyService spotifyService, IPlaylistPrototypeGenerator playlistPrototypeGenerator, SpotifyContext spotifyContext,
            IPrototypesSorter prototypesSorter, IOperationValidator operationValidator,
            IPlaylistValidator playlistValidator)
        {
            OperationManager = operationManager;
            UserManager = userManager;
            AccessTokenStore = accessTokenStore;
            SpotifyService = spotifyService;
            PlaylistPrototypeGenerator = playlistPrototypeGenerator;
            SpotifyContext = spotifyContext;
            PrototypesSorter = prototypesSorter;
            OperationValidator = operationValidator;
            PlaylistValidator = playlistValidator;
        }

        [HttpGet("operation/begin-new")]
        public async Task<IActionResult> BeginNewOperation([FromQuery(Name = "playlist_id")] string playlistId)
        {
            User user = UserManager.GetUserAsync(HttpContext.User).Result;

            SpotifyAuthorization authorization = new SpotifyAuthorization
            {
                AccessToken = await AccessTokenStore.GetAccessToken(user)
            };

            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(authorization);

            SpotifyPlaylist playlist = await playlistService.GetPlaylist(playlistId);

            PlaylistValidationResult validation = await PlaylistValidator.ValidateAsync(playlist);

            if (validation == PlaylistValidationResult.Ok)
            {
                Operation operation = new Operation
                {
                    CreatedAt = DateTime.Now,
                    OriginalPlaylistId = playlistId,
                    OwnerId = user.Id,
                    OriginalPlaylistDescription = playlist.Description,
                    OriginalPlaylistName = playlist.Name
                };

                await OperationManager.CreateAsync(operation);

                return RedirectToAction("MakePrototype", new {operation_id = operation.Id, playlist_id = playlist.Id});
            }

            else if (validation == PlaylistValidationResult.TooLarge)
            {
                return Content("Playlist is too large.\nAllowed playlist items count is 300.");
            }
            
            else if (validation == PlaylistValidationResult.Null)
            {
                return Content("Could not validate this playlist " + playlistId);
            }

            return Ok();
        }

        [HttpGet("operation/make-prototype")]
        public async Task<IActionResult> MakePrototype(MakePrototypePayload payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);
            User user = await UserManager.GetUserAsync(HttpContext.User);

            if (operation.Prototype == null)
            {
                SpotifyAuthorization auth = new SpotifyAuthorization
                {
                    AccessToken = await AccessTokenStore.GetAccessToken(user)
                };

                PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(auth);

                // TODO can be copy playlist into database.
                SpotifyPlaylist playlist = await playlistService.GetPlaylist(operation.OriginalPlaylistId);

                // int total??
                // Without him i ll be able to left behind SpotifyPlaylist (upper)
                List<SpotifyTrack> tracks = await playlistService.GetAllTracks(playlist.Id, playlist.Tracks.Total);

                operation.Prototype = await PlaylistPrototypeGenerator.GenerateAsync(tracks, operation);
                PrototypesSorter.Sort(operation.Prototype);

                await OperationManager.OperationContext.AddAsync(operation.Prototype);
                _ = OperationManager.OperationContext.SaveChangesAsync();

                // operation.Prototype.Tracks.ForEach(x => x.PlaylistPrototype = operation.Prototype);

                return View("CheckPrototype", new CheckPrototypeModel
                {
                    Prototype = operation.Prototype,
                    OperationId = (Guid) operation.Id,
                    PlaylistId = playlist.Id,
                    CurrentUser = user
                });
            }

            else
            {
                PrototypesSorter.Sort(operation.Prototype);

                SpotifyContext.Update(operation.Prototype);
                _ = SpotifyContext.SaveChangesAsync();

                return View("CheckPrototype", new CheckPrototypeModel
                {
                    Prototype = operation.Prototype,
                    OperationId = (Guid) operation.Id,
                    PlaylistId = operation.OriginalPlaylistId,
                    CurrentUser = user
                });
            }
        }

        [HttpGet("operation/name-your-playlist")]
        public IActionResult NameYourPlaylist(NameYourPlaylistPayload payload)
        {
            NameYourPlaylist model = new NameYourPlaylist
            {
                PlaylistId = payload.PlaylistId,
                OperationId = payload.OperationId,
                CurrentUser = UserManager.GetUserAsync(HttpContext.User).Result
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

            return RedirectToAction("Summary", new {operation_id = operation.Id});
        }

        [HttpGet("operation/summary")]
        public async Task<IActionResult> Summary([FromQuery(Name = "operation_id")] Guid operationId)
        {
            return View("Summary", new SummaryOperationModel
            {
                Operation = await OperationManager.GetAsync(operationId),
                OperationId = operationId,
                CurrentUser = await UserManager.GetUserAsync(HttpContext.User)
            });
        }

        [HttpGet("operation/execute")]
        public async Task<IActionResult> ExecuteOperation(ExecuteOperationPayload payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);
            User user = await UserManager.GetUserAsync(HttpContext.User);

            OperationValidationResult validationResult = await OperationValidator.ValidateAsync(operation);

            if (validationResult == OperationValidationResult.Ok)
            {
                SpotifyAuthorization auth = new SpotifyAuthorization
                {
                    AccessToken = await AccessTokenStore.GetAccessToken(user)
                };

                ExecuteResult result = await Executor.ExecuteAsync(operation, operation.Prototype, user, auth);

                CompletedPlaylist completedPlaylist = await CompletedPlaylistGenerator.GenerateAsync(operation.Prototype, result.Playlist, user);

                ExecuteSuccessfullyModel model = new ExecuteSuccessfullyModel
                {
                    Operation = operation,
                    CurrentUser = user,
                    CompletedPlaylist = completedPlaylist,
                    SpotifyPlaylist = result.Playlist
                };
                
                return View("ExecutedSuccessfully", model);
            }

            return Content($"Could not validate operation {operation.Id}.\n{validationResult.ToString()}");
        }

        [HttpPost("operation/summary/confirm")]
        public async Task<IActionResult> ConfirmOperationPost(SummaryOperationModel model)
        {
            SimpleOperation op = await OperationManager.GetSimpleAsync(model.OperationId);
            op.IsSubmitted = true;
            op.SubmittedAt = DateTime.Now;

            await OperationManager.UpdateAsync((Operation) op);

            return RedirectToAction("ExecuteOperation", "Operation", new {operation_id = model.OperationId});
        }
    }
}