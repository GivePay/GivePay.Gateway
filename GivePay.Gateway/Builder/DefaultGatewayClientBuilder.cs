using System;
using GivePay.Gateway.Ach.Client;
using GivePay.Gateway.OAuth;
using GivePay.Gateway.Transactions.Client;

namespace GivePay.Gateway.Builder
{
    public sealed class DefaultGatewayClientBuilder : IGatewayClientBuilder
    {
        public Uri BaseUri { get; private set; }

        public Uri OAuthAuthority { get; private set; }

        public string ClientId { get; private set; }

        private string ClientSecret { get; set; }

        private string Scopes { get; set; }

        public IGatewayClientBuilder WithBaseUri(Uri baseUri)
        {
            BaseUri = baseUri;

            return this;
        }

        public IGatewayClientBuilder WithOAuthCredentials(Uri authority, string clientId, string clientSecret, string scopes)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            OAuthAuthority = authority;
            Scopes = scopes;

            return this;
        }

        private void ThrowValidationExceptions()
        {
            if (BaseUri == null)
            {
                throw new InvalidOperationException("BaseUri cannot be null. Call WithBaseUri(Uri baseUri)");
            }

            if (OAuthAuthority == null)
            {
                throw new InvalidOperationException("OAuthAuthority cannot be null. Call WithOAuthCredentials(Uri authority, string clientId, string clientSecret)");
            }

            if (string.IsNullOrWhiteSpace(ClientId))
            {
                throw new InvalidOperationException("ClientId cannot be null. Call WithOAuthCredentials(Uri authority, string clientId, string clientSecret)");
            }

            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                throw new InvalidOperationException("ClientSecret cannot be null. Call WithOAuthCredentials(string clientId, string clientSecret)");
            }
        }

        public ITransactionClient BuildTransactionClient()
        {
            ThrowValidationExceptions();

            var accessTokenUriBuilder = new UriBuilder(OAuthAuthority);
            accessTokenUriBuilder.Path = Urls.TokenEndpoint;

            return new DefaultTransactionClient(
                BaseUri,
                new DefaultAccessTokenProvider(ClientId, ClientSecret, Scopes, accessTokenUriBuilder.Uri)
            );
        }

        public IAchClient BuildAchClient()
        {
            ThrowValidationExceptions();

            var accessTokenUriBuilder = new UriBuilder(OAuthAuthority);
            accessTokenUriBuilder.Path = Urls.TokenEndpoint;

            return new DefaultAchClient(
                BaseUri,
                new DefaultAccessTokenProvider(ClientId, ClientSecret, Scopes, accessTokenUriBuilder.Uri)
            );
        }
    }
}
