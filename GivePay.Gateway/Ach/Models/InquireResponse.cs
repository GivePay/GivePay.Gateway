using System.Runtime.Serialization;

namespace GivePay.Gateway.Ach.Models
{
    [DataContract]
    public sealed class InquireResponse
    {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "return_code")]
        public string ReturnCode { get; set; }
    }
}
