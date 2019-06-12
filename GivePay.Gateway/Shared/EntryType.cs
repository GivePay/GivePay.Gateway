using System.Runtime.Serialization;

namespace GivePay.Gateway
{
    public enum EntryType
    {
        [EnumMember(Value = "com.givepay.transactions.entry-method.mag-stripe")]
        MagneticStripe,

        [EnumMember(Value = "com.givepay.transactions.entry-method.emv")]
        Emv,

        [EnumMember(Value = "com.givepay.transactions.entry-method.nfc")]
        Nfc,

        [EnumMember(Value = "com.givepay.transactions.entry-method.mobile")]
        Mobile,

        [EnumMember(Value = "com.givepay.transactions.entry-method.keypad")]
        Keypad,
    }
}
