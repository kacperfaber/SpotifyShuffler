using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

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

        [ForeignKey("ResponseId")]
        public ApiResponse Response { get; set; }

        public Guid ResponseId { get; set; }

        [ForeignKey("AuthorizationId")]
        public Authorization Authorization { get; set; }

        public Guid AuthorizationId { get; set; }

        [ForeignKey("AuthorizedUserId")]
        public User AuthorizedUser { get; set; }

        public Guid AuthorizedUserId { get; set; }
    }
}