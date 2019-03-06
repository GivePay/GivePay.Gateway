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
    }
}
