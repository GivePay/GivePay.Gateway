using System.Threading.Tasks;

namespace GivePay.Gateway.OAuth
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}
