using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GivePay.Gateway.Transactions
{
    /// <summary>
    /// The response from a transaction lookup request
    /// </summary>
    [DataContract(Name = "lookup_response")]
    public sealed class LookupResponse : BaseTransactionResponse
    {
        /// <summary>
        /// The timestamp for the time of transaction
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The type of transaction
        /// </summary>
        [DataMember(Name = "transaction_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// The amount of the transaction
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// The MID
        /// </summary>
        [DataMember(Name = "mid")]
        public string MerchantId { get; set; }

        /// <summary>
        /// The TID
        /// </summary>
        [DataMember(Name = "tid")]
        public string TerminalId { get; set; }

        /// <summary>
        /// The status of the transaction
        /// </summary>
        [DataMember(Name = "transaction_status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionStatus TransactionStatus { get; set; }

        /// <summary>
        /// Auth Code
        /// </summary>
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }

        /// <summary>
        /// AVS Result
        /// </summary>
        [DataMember(Name = "avs_result")]
        public string AvsResult { get; set; }

        /// <summary>
        /// CVV/CVV2 Result
        /// </summary>
        [DataMember(Name = "cvv_result")]
        public string CvvResult { get; set; }
    }
}
