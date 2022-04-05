using System.Runtime.Serialization;

namespace Trainer.Enums
{
    public enum StatusUser
    {
        [EnumMember(Value = "Active")]
        Active = 0,

        [EnumMember(Value = "Block")]
        Block = 1,

        [EnumMember(Value = "Delete")]
        Delete = 2,

        [EnumMember(Value = "Pending")]
        Pending =3,

        [EnumMember(Value = "WaitEmailConfirm")]
        WaitEmailConfirm = 5,

        [EnumMember(Value = "Decline")]
        Decline = 4,
    }
}
