using System;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Payloads
{
    public class NameYourPlaylistPayload
    {
        [FromQuery(Name = "operation_id")]
        public Guid OperationId { get; set; }
        
        [FromQuery(Name = "playlist_id")]
        public string PlaylistId { get; set; }
    }
}