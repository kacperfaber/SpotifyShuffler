using System;
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

        public OperationController(OperationManager operationManager, UserManager userManager, IAccessTokenStore accessTokenStore)
        {
            OperationManager = operationManager;
            UserManager = userManager;
            AccessTokenStore = accessTokenStore;
        }

        [HttpGet("operation/begin-new")]
        public async Task<IActionResult> BeginNewOperation(string playlistId)
        {
            User user = UserManager.GetUserAsync(HttpContext.User).Result;

            string accessToken = await AccessTokenStore.GetAccessToken(user);

            SpotifyAuthorization spotifyAuthorization = new SpotifyAuthorization() {AccessToken = accessToken};

            PlaylistService service =
                new PlaylistService(new SpotifyClient(new InstanceToDictionaryConverter(), new QueryGenerator(new QueryParameterGenerator())),
                    new TrackUriGenerator());
            service.Authorization = spotifyAuthorization;

            SpotifyPlaylist playlist = await service.GetPlaylist(playlistId);

            Operation operation = new Operation
            {
                CreatedAt = DateTime.Now,
                OriginalPlaylistId = playlistId,
                User = user
            };

            await OperationManager.CreateAsync(operation);

            return Content($"created new operation {operation.Id},\n related with {playlist.Name} playlist owned by {playlist.Owner.Name}");
        }

        [HttpGet("operation/make-prototype")]
        public async Task<IActionResult> MakePrototype(MakePrototypePayload payload)
        {
            Operation operation = await OperationManager.GetAsync(payload.OperationId);

            // Generate prototype,
            // Show to user in form, and ask 'Do you accept' or something.
            // If not refresh page.

            return Content("Do you accept that prototype?");
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