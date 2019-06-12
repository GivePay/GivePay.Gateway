using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    [DataContract(Name = "address")]
    public sealed class Address
    {
        [DataMember(Name = "line_1")]
        public string Line1 { get; set; }

        [DataMember(Name = "line_2")]
        public string Line2 { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [Required]
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }
    }
}
