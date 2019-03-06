using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "token_reponse")]
    public sealed class TokenResponse : BaseTransactionResponse
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
