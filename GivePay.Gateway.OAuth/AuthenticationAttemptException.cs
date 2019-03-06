using System;

namespace GivePay.Gateway.OAuth
{
    public sealed class AuthenticationAttemptException : Exception
    {
        public AuthenticationAttemptException(string message) : base(message)
        {
            
        }
    }
}
