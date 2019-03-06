using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "capture_request")]
    public sealed class CaptureRequest
    {
        [Required]
        [DataMember(Name = "mid")]
        public string Mid { get; set; }

        [Required]
        [DataMember(Name = "terminal")]
        public Terminal Terminal { get; set; }

        [Required]
        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }
    }
}
