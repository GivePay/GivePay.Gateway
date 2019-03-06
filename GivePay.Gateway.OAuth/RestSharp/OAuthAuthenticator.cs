using RestSharp;
using RestSharp.Authenticators;

namespace GivePay.Gateway.OAuth.RestSharp
{
    public sealed class OAuthAuthenticator : IAuthenticator
    {
        private readonly IAccessTokenProvider _tokenProvider;

        public OAuthAuthenticator(IAccessTokenProvider provider)
        {
            _tokenProvider = provider;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var token = _tokenProvider.GetAccessTokenAsync().Result;
            request.AddOrUpdateParameter("Authorization", $"Bearer {token}", ParameterType.HttpHeader);
        }
    }
}
