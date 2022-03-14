namespace Trainer.OpenIdDict.Controllers
{
    using Application.Exceptions;
    using Application.Interfaces;
    using Common.TableConnect.Common;
    using Domain.Entities;
    using global::OpenIddict.Abstractions;
    using global::OpenIddict.Server.AspNetCore;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using Trainer.Common;
    using Trainer.Enums;
    using static global::OpenIddict.Abstractions.OpenIddictConstants;

    public class AuthorizationController : Controller
    {
        private readonly ITrainerDbContext _dbContext;

        public AuthorizationController(
            ITrainerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost("/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = this.HttpContext.GetOpenIddictServerRequest();

            if (request.IsPasswordGrantType())
            {
                switch (request.ClientId)
                {
                    case "trainer-user":
                        return await this.SignInUser(request, null);
                    case "trainer-admin":
                        return await this.SignInUser(request, Enums.UserRole.Admin.ToName());
                }
            }
            else if (request.IsRefreshTokenGrantType())
            {
                var info = await this.HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                var id = Guid.Parse(info.Principal.Claims.First(x => x.Type == Claims.Subject).Value);
                var user = await this._dbContext.BaseUsers
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                var role = info.Principal.Claims.First(x => x.Type == Claims.Role).Value;

                if (user == null)
                {
                    var properties = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The refresh token is no longer valid."
                    }!);

                    return this.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                var claimsPrincipal = this.GetClaimsPrincipal(user, request, role);

                return this.SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new NotImplementedException("The specified grant type is not implemented.");
        }

        private ClaimsPrincipal GetClaimsPrincipal(BaseUser user, OpenIddictRequest request, string role)
        {
            var identity = new ClaimsIdentity(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                Claims.Name, Claims.Role);

            identity.AddClaim(new Claim(Claims.Subject, user.Id.ToString()));
            identity.AddClaim(new Claim(Claims.Role, role));

            if (!string.IsNullOrEmpty(user.Email))
            {
                identity.AddClaim(new Claim(Claims.Email, user.Email));
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            claimsPrincipal.SetScopes(new[]
            {
                Scopes.OfflineAccess, Scopes.OpenId, Scopes.Email, Scopes.Profile, Scopes.Roles
            }.Intersect(request.GetScopes()));

            foreach (var claim in claimsPrincipal.Claims)
            {
                claim.SetDestinations(this.GetDestinations(claim, claimsPrincipal));
            }

            return claimsPrincipal;
        }

        private IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principal)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Profile))
                    {
                        yield return Destinations.IdentityToken;
                    }

                    yield break;

                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Email))
                    {
                        yield return Destinations.IdentityToken;
                    }

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (principal.HasScope(Scopes.Roles))
                    {
                        yield return Destinations.IdentityToken;
                    }

                    yield break;

                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                case "AspNet.Identity.SecurityStamp": yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }

        private async Task<IActionResult> SignInUser(OpenIddictRequest request, string? role)
        {
            var user = await this._dbContext.BaseUsers
                .Where(x => role == Enums.UserRole.Admin.ToName() ? x.UserRole == Enums.UserRole.Admin : x.UserRole != Enums.UserRole.Admin)
                .Where(x => x.Email == request.Username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "The username/password couple is invalid."
                }!);

                return this.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var isValid = await this.ValidateCode(request, user);

            if (!isValid)
            {
                throw new ValidationException();
            }

            if (!CryptoHelper.VerifyHashedPassword(user.PasswordHash!, request.Password!))
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "The username/password couple is invalid."
                }!);

                return this.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            role ??= this.GetRole(user.Id);

            var claimsPrincipal = this.GetClaimsPrincipal(user, request, role);

            return this.SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        private string? GetRole(Guid userId)
        {
            var user = this._dbContext.BaseUsers.Find(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(BaseUser), userId);
            }

            var result = user.UserRole.ToName();
            return result;
        }

        private async Task<bool> ValidateCode(OpenIddictRequest request, BaseUser user)
        {
            var code = request.GetParameter("code").ToString();

            var isValid = this._dbContext.OTPs
                .Where(x => x.Email == request.Username)
                .Where(x => x.IsValid == true)
                .Where(x => x.Action == OTPAction.Login)
                .Where(x => x.CreatedAt.AddHours(3) > DateTime.UtcNow)
                .Any(x => x.Value == code);

            if (isValid)
            {
                this.UnValidateCode(request);
            }

            return isValid;
        }

        private async void UnValidateCode(OpenIddictRequest request)
        {
            var codes = this._dbContext.OTPs
                .Where(x => x.Email == request.Username)
                .Where(x => x.IsValid == true)
                .Where(x => x.Action == OTPAction.Login)
                .Where(x => x.CreatedAt.AddHours(3) > DateTime.UtcNow)
                .ToList();

            foreach (var code in codes)
            {
                code.IsValid = false;
                this._dbContext.OTPs.Update(code);
            }

            this._dbContext.SaveChanges();
        }
    }
}
