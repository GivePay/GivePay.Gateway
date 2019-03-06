using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "payer")]
    public sealed class Payer
    {
        [DataMember(Name = "billing_address")]
        public Address BillingAddress { get; set; }

        [DataMember(Name = "email_address")]
        public string EmailAddress { get; set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }
    }
}
