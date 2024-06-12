using static covadis.Shared.Interfaces.ICurrentUserContext;

namespace BlazorWebApp.Context
{
    using covadis.Shared.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
    {
        public CurrentUser User => CurrentUser.FromClaimsPrincipal(httpContextAccessor.HttpContext!.User);

        public bool IsAuthenticated => httpContextAccessor.HttpContext!.User.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string roleName)
        {
            return User.Roles.Contains(roleName);
        }
    }
}
