﻿using System;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Authentication");
        }
    }
}