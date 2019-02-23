using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Handlers
{
    public class EduEmailRequirement : AuthorizationHandler<EduEmailRequirement>, IAuthorizationRequirement
    {
        private string _eduEmail;

        //public string _eduEmail { get; private set; }

        public EduEmailRequirement()
        {
        }

        public EduEmailRequirement(string eduEmail)
        {
            _eduEmail = eduEmail;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EduEmailRequirement requirement)
        {
            if(!context.User.HasClaim(e => e.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            var emailEdu = context.User.FindFirst(e => e.Type == ClaimTypes.Email).Value;

            if (emailEdu.Contains(".edu"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
