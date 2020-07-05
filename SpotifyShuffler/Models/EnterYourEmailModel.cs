using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Models
{
    public class EnterYourEmailModel : LayoutModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}