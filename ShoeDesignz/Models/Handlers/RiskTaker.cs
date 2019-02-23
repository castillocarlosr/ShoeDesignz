using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoeDesignz.Models.Handlers
{
    public class RiskTaker : AuthorizationHandler<RiskTaker> , IAuthorizationRequirement
    {
        private string _riskTaker;

        //public string _riskTaker { get; private set; }

        public RiskTaker()
        {
        }

        public RiskTaker(string riskTaker)
        {
            _riskTaker = riskTaker;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RiskTaker requirement)
        {
            if (!context.User.HasClaim(r => r.Type == ClaimTypes.Actor))
            {
                return Task.CompletedTask;
            }

            var riskTakerInput = context.User.FindFirst(r => r.Type == ClaimTypes.Actor).Value;

            if (riskTakerInput == "true")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
