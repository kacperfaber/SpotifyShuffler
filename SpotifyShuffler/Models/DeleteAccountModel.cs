using System.ComponentModel.DataAnnotations;

namespace SpotifyShuffler.Models
{
    public class DeleteAccountModel : LayoutModel
    {
        [Required]
        [EmailAddress]
        public string CurrentEmailAddress { get; set; }
    }
}