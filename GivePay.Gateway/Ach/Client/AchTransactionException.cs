using System;

namespace GivePay.Gateway.Ach.Client
{
    public sealed class AchTransactionException : Exception
    {
        public string Code { get; }

        public AchTransactionException(string code, string message) : base(message)
        {
            Code = code;
        }

        public override string ToString()
        {
            return $"{Code}: {Message}";
        }
    }
}
