using System;

namespace GivePay.Gateway.Transactions.Client
{
    public interface IGatewayClientBuilder
    {
        /// <summary>
        /// Sets the base URI of the client
        /// </summary>
        /// <param name="baseUri"></param>
        /// <returns></returns>
        IGatewayClientBuilder WithBaseUri(Uri baseUri);

        /// <summary>
        /// Sets the OAuth2 credentials
        /// </summary>
        /// <param name="authority"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        IGatewayClientBuilder WithOAuthCredentials(Uri authority, string clientId, string clientSecret, string scopes);

        /// <summary>
        /// Creates a new client instance
        /// </summary>
        /// <returns></returns>
        ITransactionClient Build();
    }
}
