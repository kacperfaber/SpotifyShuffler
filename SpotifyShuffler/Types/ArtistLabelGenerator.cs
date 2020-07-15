using System.Text;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class ArtistLabelGenerator : IArtistLabelGenerator
    {
        public string Generate(SimpleSpotifyArtist[] artists)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < artists.Length; i++)
            {
                builder.Append(artists[i].Name);

                if (i + 1 < artists.Length)
                {
                    builder.Append(", ");
                }
            }

            return builder.ToString();
        }
    }
}