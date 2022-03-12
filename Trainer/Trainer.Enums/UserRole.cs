namespace Trainer.Enums
{
    using System.Runtime.Serialization;

    public enum UserRole
    {
        [EnumMember(Value = "admin")]
        Admin =0,

        [EnumMember(Value = "doctor")]
        Doctor =1,

        [EnumMember(Value = "manager")]
        Manager =2,

        [EnumMember(Value = "patient")]
        Patient = 3
    }
}
