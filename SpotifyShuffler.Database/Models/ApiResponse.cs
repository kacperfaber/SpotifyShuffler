using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyShuffler.Database.Models
{
    public class ApiResponse
    {
        [Key]
        public Guid Id { get; set; }

        public ApiRequest Request { get; set; }

        [ForeignKey("Request")]
        public Guid RequestId { get; set; }

        public DateTime ResponsedAt { get; set; }

        public int ResponsedStatusCode { get; set; }

        public string ResponseBody { get; set; }
    }
}