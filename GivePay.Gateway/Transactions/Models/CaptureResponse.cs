using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "capture_response")]
    public sealed class CaptureResponse : BaseTransactionResponse
    {
    }
}
