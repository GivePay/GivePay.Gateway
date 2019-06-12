using System;
using System.Threading.Tasks;
using GivePay.Gateway.OAuth;
using RestSharp;
using RestRequest = RestSharp.Serializers.Newtonsoft.Json.RestRequest;

namespace GivePay.Gateway.Transactions.Client
{
    public class DefaultTransactionClient : BaseOAuthClient, ITransactionClient
    {
        public DefaultTransactionClient(Uri baseUri, IAccessTokenProvider tokenProvider)
            : base(baseUri, tokenProvider)
        {

        }

        public Task<SaleResponse> AuthorizeAndCaptureAmountAsync(SaleRequest saleRequest)
        {
            var request = new RestRequest(Urls.TransactionsApiV1Sale, Method.POST);
            request.AddJsonBody(saleRequest);
            return MakeRestRequest<SaleResponse>(request);
        }

        public Task<AuthorizeResponse> AuthorizeAmountAsync(AuthorizeRequest authRequest)
        {
            var request = new RestRequest(Urls.TransactionsApiV1Authorize, Method.POST);
            request.AddJsonBody(authRequest);
            return MakeRestRequest<AuthorizeResponse>(request);
        }

        public Task<CaptureResponse> CaptureAmountAsync(CaptureRequest captureRequest)
        {
            var request = new RestRequest(Urls.TransactionsApiV1Capture, Method.POST);
            request.AddJsonBody(captureRequest);
            return MakeRestRequest<CaptureResponse>(request);
        }

        public Task<LookupResponse> LookUpTransactionAsync(string mid, string transactionId)
        {
            var uri = Urls.TransactionsApiV1LookUp(mid, transactionId);
            var request = new RestRequest(uri, Method.GET);
            return MakeRestRequest<LookupResponse>(request);
        }

        public Task<VoidResponse> VoidTransactionAsync(VoidRequest voidRequest)
        {
            var request = new RestRequest(Urls.TransactionsApiV1Void, Method.POST);
            request.AddJsonBody(voidRequest);
            return MakeRestRequest<VoidResponse>(request);
        }

        public Task<TokenResponse> TokenizeAccountAsync(TokenRequest tokenRequest)
        {
            var request = new RestRequest(Urls.TransactionsApiV1Tokenize, Method.POST);
            request.AddJsonBody(tokenRequest);
            return MakeRestRequest<TokenResponse>(request);
        }
    }
}
