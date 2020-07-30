using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Models
{
    public class ConfirmEmailModel : LayoutModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(".{0,6}")]
        public string Code { get; set; }
    }
}