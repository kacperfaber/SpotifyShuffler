using System;

namespace SpotifyShuffler.Payloads
{
    public class SubmitOperationPayload
    {
        public Guid OperationId { get; set; }

        public string PlaylistId { get; set; }
    }
}