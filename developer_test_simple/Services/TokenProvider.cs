using System.Text.Json;
using developer_test_simple.Models;

namespace developer_test_simple.Services
{
    public class TokenProvider
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private const string GrandType = "client_credentials";
        private const string Scope = "products";
        private const string _tokenEndpoint = "https://auth.dkhardware.com/realms/ctesting/protocol/openid-connect/token";

        public async Task<string> GetAccessToken(string client_id, string client_secret)
        {
            var dataParameters = new Dictionary<string, string>
            {
                { "client_id", client_id },
                { "client_secret", client_secret },
                { "grant_type", GrandType },
                { "scope", Scope }
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, _tokenEndpoint);
            request.Content = new FormUrlEncodedContent(dataParameters);
            var response = await _httpClient.SendAsync(request);
            var responseMessage = await response.Content.ReadAsStringAsync();
            var accessTokenResponse = JsonSerializer.Deserialize<AccessTokenResponse>(responseMessage);
            if (accessTokenResponse == null)
            {
                throw new Exception("Can not get access token");
            }
            return accessTokenResponse.AccessToken;
        }
    }
}