using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<User> UserManager;
        public IAccessTokenStore AccessTokenStore;
        public SpotifyService SpotifyService;

        public HomeController(UserManager<User> userManager, SpotifyService spotifyService, IAccessTokenStore accessTokenStore)
        {
            UserManager = userManager;
            SpotifyService = spotifyService;
            AccessTokenStore = accessTokenStore;
        }

        [HttpGet("home")]
        public async Task<IActionResult> Home()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            
            PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(new SpotifyAuthorization
            {
                AccessToken = await AccessTokenStore.GetAccessToken(user)
            });

            SimpleSpotifyPlaylist[] playlists = playlistService.GetPlaylists().Result.Items;
            
            return View(new HomeModel
            {
                CurrentUser = user,
                SpotifyPlaylists = playlists,
                CompletedPlaylists = user.CompletedPlaylists
            });
        }
    }
}