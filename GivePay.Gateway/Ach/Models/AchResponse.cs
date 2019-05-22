using System.Runtime.Serialization;

namespace GivePay.Gateway.Ach.Models
{
    [DataContract]
    public sealed class AchResponse
    {
        [DataMember(Name = "transaction_reference")]
        public string TransactionReference { get; set; }

        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }
    }
}
