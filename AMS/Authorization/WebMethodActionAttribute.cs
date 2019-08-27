using Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Authorization
{
    public class WebMethodActionAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "ModulesType";

        public WebMethodActionAttribute(ModulesType modulesType) => ModulesType = modulesType;

        // Get or set the Age property by manipulating the underlying Policy property
        public ModulesType ModulesType
        {
            get
            {
                if (Enum.TryParse(typeof(ModulesType),Policy.Substring(POLICY_PREFIX.Length), out var module))
                {
                    return (ModulesType)module;
                }
                return default(ModulesType);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}
