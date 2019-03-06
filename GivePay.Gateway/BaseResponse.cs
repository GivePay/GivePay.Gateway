using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    [DataContract(Name = "error")]
    public sealed class Error
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "details")]
        public string Details { get; set; }
    }

    [DataContract(Name = "response")]
    public sealed class BaseResponse<TResult>
        where TResult : class
    {
        [DataMember(Name = "result")]
        public TResult Result { get; set; }

        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "error")]
        public Error Error { get; set; }
    }
}
