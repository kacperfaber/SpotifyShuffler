using System;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Payloads
{
    public class ExecuteOperationPayload : LayoutModel
    {
        public Guid OperationId { get; set; }
    }
}