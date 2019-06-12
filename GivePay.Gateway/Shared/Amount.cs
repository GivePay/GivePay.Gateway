using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    [DataContract(Name = "amount")]
    public sealed class Amount
    {
        /// <summary>
        /// The base amount of the transaction in cents
        /// </summary>
        [Required]
        [DataMember(Name = "base_amount")]
        public int BaseAmount { get; set; }

        /// <summary>
        /// The surcharge applied to the transaction
        /// </summary>
        [DataMember(Name = "fee_amount")]
        public int FeeAmount { get; set; } = 0;
    }
}
