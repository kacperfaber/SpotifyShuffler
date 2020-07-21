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
        public ISpotifyUrisGenerator SpotifyUrisGenerator;
        public IPlaylistValidator PlaylistValidator;
        public IOrderedPrototypesProvider OrderedPrototypesProvider;
        public ICompletedPlaylistGenerator CompletedPlaylistGenerator;

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore,
            SpotifyService spotifyService, IPlaylistPrototypeGenerator playlistPrototypeGenerator, SpotifyContext spotifyContext,
            IPrototypesSorter prototypesSorter, ISpotifyUrisGenerator spotifyUrisGenerator, IOperationValidator operationValidator,
            IPlaylistValidator playlistValidator, IOrderedPrototypesProvider orderedPrototypesProvider, ICompletedPlaylistGenerator completedPlaylistGenerator)
        {
            OperationManager = operationManager;
            UserManager = userManager;
            AccessTokenStore = accessTokenStore;
            SpotifyService = spotifyService;
            PlaylistPrototypeGenerator = playlistPrototypeGenerator;
            SpotifyContext = spotifyContext;
            PrototypesSorter = prototypesSorter;
            SpotifyUrisGenerator = spotifyUrisGenerator;
            OperationValidator = operationValidator;
            PlaylistValidator = playlistValidator;
            OrderedPrototypesProvider = orderedPrototypesProvider;
            CompletedPlaylistGenerator = completedPlaylistGenerator;
        }

        [HttpGet("operation/begin-new")]
        public async Task<IActionResult> BeginNewOperation([FromQuery(Name = "playlist_id")] string playlistId)
        {
            User user = UserManager.GetUserAsync(HttpContext.User).Result;

            SpotifyAuthorization authorization = new SpotifyAuthorization {AccessToken = await AccessTokenStore.GetAccessToken(user)};

            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(authorization);

            SpotifyPlaylist playlist = await playlistService.GetPlaylist(playlistId);

            PlaylistValidationResult validation = await PlaylistValidator.ValidateAsync(playlist);

            if (validation == PlaylistValidationResult.Ok)
            {
                Operation operation = new Operation
                {
                    CreatedAt = DateTime.Now,
                    OriginalPlaylistId = playlistId,
                    User = user,
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

            PlaylistPrototype prototype = operation.Prototype;

            if (prototype == null)
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

                operation.Prototype.Tracks.ForEach(x => x.PlaylistPrototype = operation.Prototype);

                SpotifyContext.Add(operation.Prototype);
                _ = SpotifyContext.SaveChangesAsync();

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
                PrototypesSorter.Sort(prototype);

                SpotifyContext.Update(prototype);
                _ = SpotifyContext.SaveChangesAsync();

                TrackPrototype track = prototype.Tracks.FirstOrDefault();

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

                PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(auth);

                SpotifyPlaylist playlist =
                    await playlistService.CreatePlaylist(user.SpotifyAccountId, operation.PlaylistName, operation.PlaylistDescription, true, false);

                CompletedPlaylist completedPlaylist = await CompletedPlaylistGenerator.GenerateAsync(operation.Prototype, playlist, user);
                await SpotifyContext.AddAsync(completedPlaylist);
                _ = SpotifyContext.SaveChangesAsync();

                IOrderedEnumerable<TrackPrototype> tracks = OrderedPrototypesProvider.Provide(operation.Prototype);

                IEnumerable<string> uris = SpotifyUrisGenerator.Generate(tracks);

                _ = playlistService.AddAllTracks(playlist.Id, uris);

                ExecuteSuccessfullyModel model = new ExecuteSuccessfullyModel
                {
                    Operation = operation,
                    CurrentUser = user,
                    CompletedPlaylist = completedPlaylist,
                    SpotifyPlaylist = playlist
                };
                
                return View("ExecutedSuccessfully", model);
            }

            return Content($"Could not validate operation {operation.Id}.\n{validationResult.ToString()}");
        }

        [HttpPost("operation/summary/confirm")]
        public async Task<IActionResult> ConfirmOperationPost(SummaryOperationModel model)
        {
            Operation op = await OperationManager.GetAsync(model.OperationId);
            op.IsSubmitted = true;
            op.SubmittedAt = DateTime.Now;

            await OperationManager.UpdateAsync(op);

            return RedirectToAction("ExecuteOperation", "Operation", new {operation_id = model.OperationId});
        }
    }
}