using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "order")]
    public sealed class Order
    {
        [DataMember(Name = "order_number")]
        public string OrderNumber { get; set; }
    }
}
