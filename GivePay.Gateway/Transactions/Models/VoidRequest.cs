using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using GivePay.Gateway.Shared;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "void_request")]
    public sealed class VoidRequest
    {
        [Required]
        [DataMember(Name = "mid")]
        public string Mid { get; set; }
        [Required]
        [DataMember(Name = "terminal")]
        public Terminal Terminal { get; set; }

        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }
    }
}
