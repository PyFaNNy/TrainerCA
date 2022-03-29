namespace Trainer.Infrastructure.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var idString = user?.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", StringComparison.InvariantCultureIgnoreCase))?.Value;

            if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out Guid userId) || userId == Guid.Empty)
            {
                return null;
            }

            return userId;
        }
    }
}
