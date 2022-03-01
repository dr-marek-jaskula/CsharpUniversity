using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
    }
}
