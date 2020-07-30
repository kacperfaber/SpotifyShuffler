using System;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyShuffler.Payloads
{
    public class NameYourPlaylistPayload
    {
        public Guid OperationId { get; set; }
    }
}