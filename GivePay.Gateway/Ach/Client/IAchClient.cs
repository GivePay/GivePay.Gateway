using System.Threading.Tasks;
using GivePay.Gateway.Ach.Models;

namespace GivePay.Gateway.Ach.Client
{
    public interface IAchClient
    {
        /// <summary>
        /// Places an ACH transaction for the information provided in the request
        /// </summary>
        /// <param name="request">The transaction information</param>
        /// <returns></returns>
        Task<AchResponse> PostAchAsync(AchRequest request);

        /// <summary>
        /// Retrieves information about a previous transaction
        /// </summary>
        /// <param name="mid">The merchant ID</param>
        /// <param name="tid">The terminal ID</param>
        /// <param name="transactionId">The transaction ID</param>
        /// <returns></returns>
        Task<InquireResponse> InquireAchAsync(string mid, string tid, string transactionId);
    }
}
