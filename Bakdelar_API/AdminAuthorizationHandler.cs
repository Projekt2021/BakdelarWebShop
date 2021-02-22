using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bakdelar_API
{
    public class AdminAuthorizationHandler: IAuthorizationRequirement
    {
    }

    public class AdminRequirementHandler
    : AuthorizationHandler<AdminAuthorizationHandler>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AdminAuthorizationHandler adminAccess)
        {
            // check if Role claim exists - Else Return
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
                return Task.CompletedTask;

            // claim exists - retrieve the value
            var claim = context.User
                .Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            var role = claim.Value;

            // check if the claim equals to either Admin or Editor
            // if satisfied, set the requirement as success
            if (role == "Admin")
                context.Succeed(adminAccess);

            return Task.CompletedTask;
        }
    }
}
