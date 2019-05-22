using System.Runtime.Serialization;

namespace GivePay.Gateway.Ach.Models
{
    [DataContract]
    public sealed class WebhookMeta
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }
    }
}
