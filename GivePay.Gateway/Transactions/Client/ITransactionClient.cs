using GivePay.Gateway.Transactions;
using System.Threading.Tasks;

namespace GivePay.Gateway.Transactions.Client
{
    public interface ITransactionClient
    {
        /// <summary>
        /// Authorizes and captures the amount for the given card.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SaleResponse> AuthorizeAndCaptureAmountAsync(SaleRequest request);

        /// <summary>
        /// Authorizes a transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthorizeResponse> AuthorizeAmountAsync(AuthorizeRequest request);

        /// <summary>
        /// Captures a previously authorized transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CaptureResponse> CaptureAmountAsync(CaptureRequest request);

        /// <summary>
        /// Looks up a transaction
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task<LookupResponse> LookUpTransactionAsync(string mid, string transactionId);

        /// <summary>
        /// Voids a transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VoidResponse> VoidTransactionAsync(VoidRequest request);

        /// <summary>
        /// Transforms a payment method into a token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TokenResponse> TokenizeAccountAsync(TokenRequest request);
    }
}
