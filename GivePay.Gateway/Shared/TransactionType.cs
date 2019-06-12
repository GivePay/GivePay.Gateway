using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    public enum TransactionType
    {
        /// <summary>
        /// A sale transaction
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-type.sale")]
        Sale,

        /// <summary>
        /// An authorization
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-type.auth")]
        Auth,

        /// <summary>
        /// A capture transaction
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-type.capt")]
        Capture,

        /// <summary>
        /// A credit/refund transaction
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-type.credit")]
        Credit,

        /// <summary>
        /// A transaction void
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-type.void")]
        Void,

        /// <summary>
        /// A tokenization transaction
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-type.tokenize")]
        Tokenize
    }
}
