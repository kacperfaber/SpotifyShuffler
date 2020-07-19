using System;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Payloads
{
    public class ExecuteOperationPayload : LayoutModel
    {
        public Guid OperationId { get; set; }

        public string PlaylistId { get; set; }
    }
}