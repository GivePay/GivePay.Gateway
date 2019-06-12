using System.Runtime.Serialization;

namespace GivePay.Gateway.Shared
{
    [DataContract]
    public sealed class BankAccountInformation
    {
        [DataMember(Name = "aba")]
        public string Aba { get; set; }

        [DataMember(Name = "account_number")]
        public string AccountNumber { get; set; }

        [DataMember(Name = "bank_name")]
        public string BankName { get; set; }

        [DataMember(Name = "account_verification")]
        public AccountVerification AccountVerification { get; set; }
    }
}
