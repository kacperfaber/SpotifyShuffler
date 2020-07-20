using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Controllers
{
    public class RaportController : Controller
    {
        public IActionResult DownloadRaport([FromQuery(Name = "completed_playlist_id")] Guid completedPlaylistId)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine($"RAPORT FOR {completedPlaylistId.ToString()}");
            writer.Flush();
            
            return File(stream, "text/plain");
        }
    }
}