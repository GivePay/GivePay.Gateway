using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "authorize_response")]
    public sealed class AuthorizeResponse : BaseTransactionResponse
    {
        /// <summary>
        /// Auth Code
        /// </summary>
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }
    }
}
