using System;

namespace GivePay.Gateway.Ach.Client
{
    public sealed class AchTransactionException : Exception
    {
        public string Code { get; }

        public string Message { get; set; }

        public AchTransactionException(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"{Code}: {Message}";
        }
    }
}
