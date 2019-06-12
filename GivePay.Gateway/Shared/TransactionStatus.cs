using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    public enum TransactionStatus
    {
        /// <summary>
        /// Indicates that the transaction has been completed 
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-status.complete")]
        Complete,

        /// <summary>
        /// Indicates that the transaction has been completed after an authorization
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-status.captured")]
        Captured,

        /// <summary>
        /// Indicates that the transaction has been voided
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-status.voided")]
        Voided,

        /// <summary>
        /// Indicates that the transaction has been declined by the processor
        /// </summary>
        [EnumMember(Value = "com.givepay.transaction-status.declined")]
        Declined
    }
}
