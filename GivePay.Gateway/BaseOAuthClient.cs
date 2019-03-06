using System;
using System.Threading.Tasks;
using GivePay.Gateway.OAuth;
using GivePay.Gateway.OAuth.RestSharp;
using RestSharp;

namespace GivePay.Gateway
{
    public abstract class BaseOAuthClient
    {
        private readonly RestClient _client;

        public BaseOAuthClient(Uri baseUri, IAccessTokenProvider tokenProvider)
        {
            _client = new RestClient(baseUri);
            _client.Authenticator = new OAuthAuthenticator(tokenProvider);
            _client.AddHandler("application/json", RestSharp.Newtonsoft.NewtonsoftJsonSerializer.Default);

            //_client.Proxy = new WebProxy("127.0.0.1", 8888);
        }

        protected async Task<TResponse> MakeRestRequest<TResponse>(IRestRequest request)
            where TResponse : class
        {
            var response = await _client.ExecuteTaskAsync<BaseResponse<TResponse>>(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            CheckForErrors(response);

            return response.Data.Result;
        }

        protected void CheckForErrors<TResponse>(IRestResponse<TResponse> response)
        {
            if (!response.IsSuccessful)
            {
                throw new Exception(response.StatusDescription, new Exception(response.Content));
            }

            if (response.Data == null)
            {
                throw new Exception(response.Content);
            }
        }
    }
}
