using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
