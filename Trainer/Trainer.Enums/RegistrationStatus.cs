namespace Prixy.Enums
{
    public enum RegistrationStatus
    {
        Active = 1,
        Email,
        PendingEmailVerification,
        PendingKYC,
        PendingApproval,
        ReadyToStart,
        Declined
    }
}
