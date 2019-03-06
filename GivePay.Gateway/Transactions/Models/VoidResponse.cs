using System.Runtime.Serialization;

namespace GivePay.Gateway.Transactions
{
    [DataContract(Name = "void_response")]
    public sealed class VoidResponse : BaseTransactionResponse
    {
    }
}
