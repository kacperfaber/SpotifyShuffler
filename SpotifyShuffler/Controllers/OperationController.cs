﻿using System;
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
    [Authorize]
    public class OperationController : Controller
    {
        public IAccessTokenStore AccessTokenStore;
        public ICompletedPlaylistGenerator CompletedPlaylistGenerator;
        public Executor Executor;
        public OperationManager OperationManager;
        public IOperationValidator OperationValidator;
        public IPlaylistPrototypeGenerator PlaylistPrototypeGenerator;
        public IPlaylistValidator PlaylistValidator;
        public IPrototypesSorter PrototypesSorter;
        public SpotifyContext SpotifyContext;
        public SpotifyService SpotifyService;
        public UserManager UserManager;
        public IPlaylistCollaborativeChecker PlaylistCollaborativeChecker;

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore,
            SpotifyService spotifyService, SpotifyContext spotifyContext,
            IOperationValidator operationValidator,
            IPlaylistValidator playlistValidator, ICompletedPlaylistGenerator completedPlaylistGenerator, Executor executor, IPlaylistCollaborativeChecker playlistCollaborativeChecker)
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
                    OriginalPlaylistName = playlist.Name,
                    Kind = OperationKind.CreateNewPlaylist
                };

                await OperationManager.CreateAsync(operation);

                return RedirectToAction("MakePrototype", new {operation_id = operation.Id, playlist_id = playlist.Id});
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

        [HttpGet("operation/make-prototype")]
        public async Task<IActionResult> MakePrototype(MakePrototypePayload payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);
            User user = await UserManager.GetUserAsync(HttpContext.User);

            if (operation.Prototype == null)
            {
                SpotifyAuthorization auth = SpotifyAuthorization.Create(await AccessTokenStore.GetAccessToken(user));

                PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(auth);

                SpotifyPlaylist playlist = await playlistService.GetPlaylist(operation.OriginalPlaylistId);

                List<SpotifyTrack> tracks = await playlistService.GetAllTracks(playlist.Id, playlist.Tracks.Total);

                operation.Prototype = await PlaylistPrototypeGenerator.GenerateAsync(tracks, operation);
                PrototypesSorter.Sort(operation.Prototype);

                await OperationManager.SpotifyContext.AddAsync(operation.Prototype);
                _ = OperationManager.SpotifyContext.SaveChangesAsync();

                return RedirectToAction("CheckPrototype", new
                {
                    operation_id = operation.Id,
                    playlist_id = operation.OriginalPlaylistId
                });
            }

            else
            {
                return BadRequest($"Taken operation {{ {operation.Id} }} has prototype.");
            }
        }

        [HttpGet("operation/check-prototype")]
        public async Task<IActionResult> CheckPrototype(
            [FromQuery(Name = "operation_id")] Guid operationId,
            [FromQuery(Name = "playlist_id")] string playlistId)
        {
            Operation operation = await OperationManager.GetAsync(operationId);
            User user = await UserManager.GetUserAsync(HttpContext.User);
            SpotifyAuthorization authorization = SpotifyAuthorization.Create(await AccessTokenStore.GetAccessToken(user));

            PlaylistService service = await SpotifyService.GetAsync<PlaylistService>(authorization);
            SpotifyAccount spotifyAccount = SpotifyContext.SpotifyAccounts.FirstOrDefault(x => x.SpotifyId == user.SpotifyAccountId);
            bool isCollaborative = PlaylistCollaborativeChecker.Check(spotifyAccount, await service.GetPlaylist(playlistId));

            return View("CheckPrototype", new CheckPrototypeModel
            {
                CurrentUser = user,
                Prototype = operation.Prototype,
                OperationId = (Guid) operation.Id,
                PlaylistId = operation.OriginalPlaylistId,
                PlaylistIsCollaborative = isCollaborative
            });
        }

        [HttpGet("operation/refresh-prototype")]
        public async Task<IActionResult> RefreshPrototype([FromQuery(Name = "operation_id")] Guid operationId)
        {
            Operation operation = await OperationManager.GetAsync(operationId);

            PrototypesSorter.Sort(operation.Prototype);

            SpotifyContext.Update(operation.Prototype);
            _ = SpotifyContext.SaveChangesAsync();

            return RedirectToAction("CheckPrototype", new {operation_id = operation.Id, playlist_id = operation.OriginalPlaylistId});
        }

        [HttpGet("operation/configure-your-playlist")]
        public IActionResult ConfigureYourPlaylist(NameYourPlaylistPayload payload)
        {
            NameYourPlaylist model = new NameYourPlaylist
            {
                PlaylistId = payload.PlaylistId,
                OperationId = payload.OperationId,
                CurrentUser = UserManager.GetUserAsync(HttpContext.User).Result
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

            return RedirectToAction("Summary", new {operation_id = operation.Id});
        }

        [HttpGet("operation/summary")]
        public async Task<IActionResult> Summary([FromQuery(Name = "operation_id")] Guid operationId)
        {
            Operation operation = await OperationManager.GetAsync(operationId);

            if (operation.Kind == OperationKind.CreateNewPlaylist)
            {
                return View("CreateNewPlaylistSummary", new SummaryOperationModel
                {
                    Operation = operation,
                    OperationId = operationId,
                    CurrentUser = await UserManager.GetUserAsync(HttpContext.User)
                });
            }

            else if (operation.Kind == OperationKind.UseOriginalPlaylist)
            {
                return View("UseOriginalPlaylistSummary", new SummaryOperationModel
                {
                    Operation = operation,
                    OperationId = operationId,
                    CurrentUser = await UserManager.GetUserAsync(HttpContext.User)
                });
            }

            return BadRequest();
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

        [HttpGet("operation/set-kind")]
        public IActionResult ConfigureOperationKind([FromQuery(Name = "operation_id")] Guid operationId, [FromQuery(Name = "playlist_id")] string playlistId)
        {
            User user = UserManager.GetUserAsync(HttpContext.User).Result;
            return View(new ConfigureOperationKindModel {OperationId = operationId, PlaylistId = playlistId, CurrentUser = user});
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureOperationKindPost(ConfigureOperationKindModel model)
        {
            SimpleOperation op = await OperationManager.GetSimpleAsync(model.OperationId);
            op.Kind = model.CreateNewPlaylist ? OperationKind.CreateNewPlaylist : OperationKind.UseOriginalPlaylist;
            _ = OperationManager.SpotifyContext.SaveChangesAsync();

            if (model.CreateNewPlaylist)
            {
                return RedirectToAction("ConfigureYourPlaylist", new {operation_id = model.OperationId, playlist_id = model.PlaylistId});
            }

            else
            {
                return RedirectToAction("Summary", new {operation_id = model.OperationId});
            }
        }
    }
}