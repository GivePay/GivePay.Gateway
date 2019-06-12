using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    public enum TerminalType
    {
        [EnumMember(Value = "com.givepay.terminal-types.ecommerce")]
        ECommerce,

        [EnumMember(Value = "com.givepay.terminal-types.physical")]
        Physical,

        [EnumMember(Value = "com.givepay.terminal-types.phone")]
        Phone,

        [EnumMember(Value = "com.givepay.terminal-types.mail")]
        Mail
    }
}
