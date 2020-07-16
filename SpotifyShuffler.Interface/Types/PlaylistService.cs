using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class PlaylistService
    {
        public Authorization Authorization { get; set; }

        public Paging<SimpleSpotifyPlaylist> GetPlaylists(string spotifyId)
        {
            throw new NotImplementedException();
        }

        public SpotifyPlaylist GetPlaylist(string playlistId)
        {
            throw new NotImplementedException();
        }

        public async Task<SpotifyPlaylist> CreatePlaylist(string userId, string name, string description, bool @public)
        {
            CreatePlaylistPayload payload = new CreatePlaylistPayload
            {
                Collaborative = false,
                Description = description,
                Name = name,
                IsPublic = @public
            };

            string requestBody = JsonConvert.SerializeObject(payload);

            using (HttpClient http = new HttpClient())
            {
                StringContent stringContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://api.spotify.com/v1/users/{userId}/playlists")
                {
                    Content = stringContent
                };
                
                request.Headers.Add("Authorization", Authorization.GetToken());

                HttpResponseMessage responseMessage = await http.SendAsync(request);

                return JsonConvert.DeserializeObject<SpotifyPlaylist>(await responseMessage.Content.ReadAsStringAsync());
            }
        }

        public void AddTracks(SpotifyPlaylist playlist, params SimpleSpotifyTrack[] tracks)
        {
            throw new NotImplementedException();
        }
    }
}