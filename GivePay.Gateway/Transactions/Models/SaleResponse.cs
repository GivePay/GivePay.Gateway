using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "sale_response")]
    public sealed class SaleResponse : BaseTransactionResponse
    {
        /// <summary>
        /// Auth Code
        /// </summary>
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }

        /// <summary>
        /// AVS Result
        /// </summary>
        [DataMember(Name = "avs_result")]
        public string AvsResult { get; set; }

        /// <summary>
        /// CVV/CVV2 Result
        /// </summary>
        [DataMember(Name = "cvv_result")]
        public string CvvResult { get; set; }
    }
}
