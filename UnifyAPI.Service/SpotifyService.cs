using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAPI.Service
{
    public class SpotifyService : ISpotifyService
    {

        public SpotifyServiceOptions Options { get; set; }
        private readonly HttpClient _client;

        public SpotifyService(IOptions<SpotifyServiceOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
            _client = new HttpClient();
        }

        public async Task SetAuthorization(string code)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", Options.RedirectUri),
                new KeyValuePair<string, string>("client_id", Options.ClientId),
                new KeyValuePair<string, string>("client_secret", Options.ClientSecret),
            });

            var tokenRequest = await _client.PostAsync("https://accounts.spotify.com/api/token", content);
            var response = await tokenRequest.Content.ReadAsStringAsync();
            var auth = JsonConvert.DeserializeObject<AuthParams>(response);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.AccessToken);
        }
    }
}
