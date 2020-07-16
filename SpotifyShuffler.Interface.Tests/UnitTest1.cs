using System.Threading.Tasks;
using NUnit.Framework;

namespace SpotifyShuffler.Interface.Tests
{
    public class Tests
    {
        [Test]
        public async Task Test1()
        {
            PlaylistService service = new PlaylistService
            {
                Authorization = new Authorization()
                {
                    AccessToken = "BQC9wct84_OMPCzEnJBPlqkDUT3ki8ubpGFYI0nk8c4iD351FEtBAuGA-ULc2krgJPm-c-N_hM7ntbIB57VqP-feg5LZxXsAO0zMZPFR0HfoKXalz_-RT2oPWuhKrRIZtife7QC4fElPMLAVd2cJgYUNA9fSZPorozaD7lvMTE09oA1MjaIdn3fr-0e_B1koNa01oZ3McQbef4G2NegYM02r6QUo7ZeHLasL9g"
                }
            };

            SpotifyPlaylist playlist = await service.CreatePlaylist("21xj5ayombftvbhumxwzprzwy", "Playlista wakacyjna!",
                "This is first test playlist using PlaylistService.", false);
        }
    }
}