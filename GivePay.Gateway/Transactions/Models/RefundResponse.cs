using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "credit_response")]
    public sealed class RefundResponse : BaseTransactionResponse
    {
        /// <summary>
        /// Auth Code
        /// </summary>
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }
    }
}
