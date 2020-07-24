using System;
using System.Collections.Generic;
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

        [HttpGet("home", Name = "home/home")]
        public async Task<IActionResult> Home()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                PlaylistService playlistService = await SpotifyService.GetAsync<PlaylistService>(new SpotifyAuthorization
                {
                    AccessToken = await AccessTokenStore.GetAccessToken(user)
                });

                Paging<SimpleSpotifyPlaylist> paging = await playlistService.GetPlaylists();
                SimpleSpotifyPlaylist[] playlists = paging.Items;

                return View("Home", new HomeModel
                {
                    CurrentUser = user,
                    Playlists = Array.ConvertAll(playlists, p => new PlaylistModel {SpotifyPlaylist = p})
                });
            }

            return View("AnonymousHome", new HomeModel());
        }
    }
}