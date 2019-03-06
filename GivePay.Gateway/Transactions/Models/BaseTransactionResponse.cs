using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "base_transaction_response")]
    public abstract class BaseTransactionResponse
    {
        [DataMember(Name = "transaction_id")]
        public string TransactionId { get; set; }

        [DataMember(Name = "result_code")]
        public int ResultCode { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
