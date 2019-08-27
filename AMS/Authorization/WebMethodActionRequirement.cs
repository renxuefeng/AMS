using Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Authorization
{
    internal class WebMethodActionRequirement : IAuthorizationRequirement
    {
        public ModulesType ModulesType { get; private set; }

        public WebMethodActionRequirement(ModulesType modulesType) { ModulesType = modulesType; }
    }
}
