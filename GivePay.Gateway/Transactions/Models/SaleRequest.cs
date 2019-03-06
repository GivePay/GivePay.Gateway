using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "sale_request")]
    public sealed class SaleRequest
    {
        [Required]
        [DataMember(Name = "mid")]
        public string Mid { get; set; }

        [Required]
        [DataMember(Name = "amount")]
        public Amount Amount { get; set; }

        [Required]
        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [Required]
        [DataMember(Name = "terminal")]
        public Terminal Terminal { get; set; }

        [DataMember(Name = "payer")]
        public Payer Payer { get; set; }
    }
}
