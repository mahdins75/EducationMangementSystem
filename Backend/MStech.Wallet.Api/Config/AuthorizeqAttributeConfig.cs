using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Implementation.AccessLinkeRoles;
using Implementation.TokenManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Azure.Cosmos;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Custom
{

    public class SessionRequirementAttribute : TypeFilterAttribute
    {
        public SessionRequirementAttribute() : base(typeof(SessionRequirmentFilter)) { }
    }

    public class SessionRequirmentFilter : IAuthorizationFilter
    {
        private readonly AccessLinkRolesService accessLinkRolesService;
        public SessionRequirmentFilter(AccessLinkRolesService accessLinkRolesService)
        {
            this.accessLinkRolesService = accessLinkRolesService;
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
           .Any(em => em is AllowAnonymousAttribute);


            var headers = context.HttpContext.Request.Headers;


            if (!allowAnonymous && context.HttpContext.User.Identity.IsAuthenticated)
            {
                var routePath = context.HttpContext.Request.Path;

                var userRoles = context.HttpContext.User?.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToList();
                if (userRoles != null && userRoles.Any())
                {
                    // Call service to check if user has access to the route
                    var hasAccess = accessLinkRolesService.CheckRoleHasUrl(routePath, userRoles).Result;
                    if (!hasAccess)
                    {
                        // Deny access if the role does not match
                        context.Result = new ForbidResult();
                    }
                }
                else
                {
                    // No roles, deny access
                    context.Result = new ForbidResult();
                }
            }
            else if (allowAnonymous)
            {
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}