using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpotifyShuffler.Database.Enums;

namespace SpotifyShuffler.Database.Models
{
    public class ApiRequest
    {
        [Key]
        public Guid Id { get; set; }

        public string Url { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public string RequestBody { get; set; }

        public DateTime? SendedAt { get; set; }

        public ApiResponse Response { get; set; }

        [ForeignKey("Response")]
        public Guid ResponseId { get; set; }

        public Authorization Authorization { get; set; }

        [ForeignKey("Authorization")]
        public Guid AuthorizationId { get; set; }

        public User AuthorizedUser { get; set; }

        [ForeignKey("AuthorizedUser")]
        public Guid AuthorizedUserId { get; set; }
    }
}