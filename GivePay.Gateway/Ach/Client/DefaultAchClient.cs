using System;
using System.Threading.Tasks;
using GivePay.Gateway.Ach.Models;
using GivePay.Gateway.OAuth;
using RestSharp;
using RestRequest = RestSharp.Serializers.Newtonsoft.Json.RestRequest;

namespace GivePay.Gateway.Ach.Client
{
    public class DefaultAchClient : BaseOAuthClient, IAchClient
    {
        public DefaultAchClient(Uri baseUri, IAccessTokenProvider tokenProvider)
            : base(baseUri, tokenProvider)
        {

        }

        public Task<InquireResponse> InquireAchAsync(string mid, string tid, string transactionId)
        {
            var uri = Urls.AchApiV1Inquire(mid, tid, transactionId);
            var request = new RestRequest(uri, Method.GET);
            return MakeRestRequest<InquireResponse>(request);
        }

        public Task<AchResponse> PostAchAsync(AchRequest postRequest)
        {
            var request = new RestRequest(Urls.AchApiV1Post, Method.POST);
            request.AddJsonBody(postRequest);
            return MakeRestRequest<AchResponse>(request);
        }

        protected override void CheckForErrors<TResponse>(IRestResponse<BaseResponse<TResponse>> response)
        {
            if (!response.IsSuccessful)
            {
                if (response.Data?.Error != null)
                {
                    var error = response.Data.Error;
                    throw new AchTransactionException(error.Details, error.Message);
                }
            }

            base.CheckForErrors(response);
        }
    }
}
