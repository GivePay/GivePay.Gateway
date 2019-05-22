using System.Runtime.Serialization;

namespace GivePay.Gateway.Shared
{

    [DataContract]
    public sealed class AccountVerification
    {
        [DataMember(Name = "account_holder_name")]
        public string AccountHolderName { get; set; }

        [DataMember(Name = "dl_number")]
        public string DlNumber { get; set; }

        [DataMember(Name = "dl_state")]
        public string DlState { get; set; }

        [DataMember(Name = "ssn_last_4")]
        public string SsnLast4 { get; set; }

        [DataMember(Name = "check_number")]
        public string CheckNumber { get; set; }
    }
}
