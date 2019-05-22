using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using GivePay.Gateway.Shared;

namespace GivePay.Gateway.Ach.Models
{
    [DataContract]
    public sealed class AchRequest
    {
        [Required]
        [DataMember(Name = "mid")]
        public string Mid { get; set; }

        [Required]
        [DataMember(Name = "terminal")]
        public Terminal Terminal { get; set; }

        [Required]
        [DataMember(Name = "account")]
        public BankAccountInformation Account { get; set; }

        [Required]
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "webhook_meta")]
        public WebhookMeta WebhookMeta { get; set; }
    }
}
