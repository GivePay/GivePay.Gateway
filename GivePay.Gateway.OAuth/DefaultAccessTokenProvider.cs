using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GivePay.Gateway.OAuth
{
    public sealed class DefaultAccessTokenProvider : IAccessTokenProvider
    {
        private readonly Uri _accessTokenUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _scopes;
        private readonly HttpClient _client;

        private string _currentToken;
        private DateTime _tokenExpiry = DateTime.Now;

        public DefaultAccessTokenProvider(string clientId, string clientSecret, string scopes, Uri accessTokenUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scopes = scopes;
            _accessTokenUri = accessTokenUri;
            _client = new HttpClient();
        }

        private async Task<TokenResponse> RequestNewTokenAsync()
        {
            var request = new HttpRequestMessage
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("scopes", _scopes)
                }),
                Method = HttpMethod.Post,
                RequestUri = _accessTokenUri
            };

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new AuthenticationAttemptException($"A valid token could not be obtained. Check your credentials. Message={response.ReasonPhrase}; Content={content}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(responseString);
        }

        private static bool TokenNeedsRefresh(string token, DateTime expiry)
        {
            return string.IsNullOrWhiteSpace(token)
                   || expiry == DateTime.MinValue
                   || expiry.AddMinutes(-10) < DateTime.Now;
        }

        private async Task ResetTokenAsync()
        {
            var tokenResponse = await RequestNewTokenAsync();
            _currentToken = tokenResponse.AccessToken;
            _tokenExpiry = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (TokenNeedsRefresh(_currentToken, _tokenExpiry))
            {
                await ResetTokenAsync();
            }

            return _currentToken;
        }
    }
}