using System;
using Microsoft.AspNetCore.Mvc;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Payloads
{
    public class ExecuteOperationPayload : LayoutModel
    {
        [FromQuery(Name = "operation_id")]
        public Guid OperationId { get; set; }
    }
}