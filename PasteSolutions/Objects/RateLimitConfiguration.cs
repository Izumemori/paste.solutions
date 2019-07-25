using System;
using System.Collections.Generic;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace PasteSolutions.Objects
{
    public class CustomRateLimitConfiguration : RateLimitConfiguration
    {
        public CustomRateLimitConfiguration(IHttpContextAccessor httpContextAccessor, IOptions<IpRateLimitOptions> ipOptions, IOptions<ClientRateLimitOptions> clientOptions) : base(httpContextAccessor, ipOptions, clientOptions)
        {
        }

        public override ICounterKeyBuilder EndpointCounterKeyBuilder => new EndpointCounterKeyBuilder();
    }
}