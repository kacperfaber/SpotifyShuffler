using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;
using SpotifyShuffler.Payloads;
using SpotifyShuffler.Types;

namespace SpotifyShuffler.Controllers
{
    [Authorize(Policy = Policies.RequireConfirmedEmail)]
    public class OperationController : Controller
    {
        public IAccessTokenStore AccessTokenStore;
        public ICompletedPlaylistGenerator CompletedPlaylistGenerator;
        public Executor Executor;
        public OperationManager OperationManager;
        public IOperationValidator OperationValidator;
        public IPlaylistValidator PlaylistValidator;
        public SpotifyContext SpotifyContext;
        public SpotifyService SpotifyService;
        public UserManager UserManager;
        public IPlaylistCollaborativeChecker PlaylistCollaborativeChecker;
        public IOperationGenerator OperationGenerator;

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore,
            SpotifyService spotifyService, SpotifyContext spotifyContext,
            IOperationValidator operationValidator,
            IPlaylistValidator playlistValidator, ICompletedPlaylistGenerator completedPlaylistGenerator, Executor executor,
            IPlaylistCollaborativeChecker playlistCollaborativeChecker, IOperationGenerator operationGenerator)
        {
            OperationManager = operationManager;
            UserManager = userManager;
            AccessTokenStore = accessTokenStore;
            SpotifyService = spotifyService;
            SpotifyContext = spotifyContext;
            OperationValidator = operationValidator;
            PlaylistValidator = playlistValidator;
            CompletedPlaylistGenerator = completedPlaylistGenerator;
            Executor = executor;
            PlaylistCollaborativeChecker = playlistCollaborativeChecker;
            OperationGenerator = operationGenerator;
        }

        [HttpGet("operation/begin-new")]
        public async Task<IActionResult> BeginNewOperation([FromQuery(Name = "PlaylistId")] string playlistId)
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
                Operation operation = OperationGenerator.Generate(user, playlist);

                await OperationManager.CreateAsync(operation);

                return RedirectToAction("Summary", new
                {
                    OperationId = operation.Id
                });
            }

            else if (validation == PlaylistValidationResult.TooLarge)
            {
                return Content("Playlist is too large.\nAllowed playlist size is 300.");
            }

            else if (validation == PlaylistValidationResult.Null)
            {
                return Content("Could not validate playlist " + playlistId);
            }

            return BadRequest();
        }

        [HttpGet("operation/configure-your-playlist")]
        public async Task<IActionResult> ConfigureYourPlaylist(NameYourPlaylistPayload payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);

            NameYourPlaylist model = new NameYourPlaylist
            {
                OperationId = payload.OperationId,
                CurrentUser = await UserManager.GetUserAsync(HttpContext.User),
                PlaylistName = operation.PlaylistName,
                PlaylistDescription = operation.PlaylistDescription
            };

            return View(model);
        }

        [HttpPost("operation/configure-your-playlist/post")]
        public async Task<IActionResult> ConfigureYourPlaylistPost(NameYourPlaylist payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);

            operation.PlaylistName = payload.PlaylistName;
            operation.PlaylistDescription = payload.PlaylistDescription;

            await OperationManager.UpdateAsync(operation);

            return RedirectToAction("Summary", new {OperationId = operation.Id});
        }

        [HttpGet("operation/summary")]
        public async Task<IActionResult> Summary([FromQuery(Name = "OperationId")] Guid operationId)
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            Operation operation = await OperationManager.GetAsync(operationId);
            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(new SpotifyAuthorization
            {
                AccessToken = await AccessTokenStore.GetAccessToken(user)
            });

            bool isCollaborative = PlaylistCollaborativeChecker.Check(user.SpotifyAccount, await playlistService.GetPlaylist(operation.OriginalPlaylistId));

            return View("Summary", new SummaryOperationModel
            {
                Operation = operation,
                OperationId = operationId,
                CurrentUser = user,
                CanUseOriginalPlaylist = isCollaborative
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

                ExecuteResult result = await Executor.ExecuteAsync(operation, user, auth);

                CompletedPlaylist completedPlaylist = await CompletedPlaylistGenerator.GenerateAsync(result.Playlist, user);

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
        public async Task<IActionResult> SummaryPost(SummaryOperationModel model)
        {
            Operation operation = await OperationManager.GetAsync(model.OperationId);
            operation.IsSubmitted = true;
            operation.SubmittedAt = DateTime.Now;

            await OperationManager.UpdateAsync(operation);

            return RedirectToAction("ExecuteOperation", "Operation", new {model.OperationId});
        }

        [HttpGet("operation/set-kind")]
        public IActionResult ConfigureOperationKind([FromQuery(Name = "OperationId")] Guid operationId)
        {
            User user = UserManager.GetUserAsync(HttpContext.User).Result;
            return View(new ConfigureOperationKindModel {OperationId = operationId, CurrentUser = user});
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureOperationKindPost(ConfigureOperationKindModel model)
        {
            Operation op = await OperationManager.GetAsync(model.OperationId);
            op.Kind = model.CreateNewPlaylist ? OperationKind.CreateNewPlaylist : OperationKind.UseOriginalPlaylist;
            _ = OperationManager.SpotifyContext.SaveChangesAsync();

            return RedirectToAction("Summary", new {OperationId = model.OperationId});
        }
    }
}