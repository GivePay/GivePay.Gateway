using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "terminal")]
    public sealed class Terminal
    {
        [Required]
        [DataMember(Name = "tid")]
        public string TerminalId { get; set; }

        [DataMember(Name = "terminal_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TerminalType? TerminalType { get; set; }

        [DataMember(Name = "entry_method")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EntryType? EntryType { get; set; }
    }
}
