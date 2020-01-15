using AMS.BLL.RoleApp;
using AMS.BLL.UserApp;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMS.Authorization
{
    internal class WebMethodActionHandler : AuthorizationHandler<WebMethodActionRequirement>
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IRoleAppService _roleAppService;
        public WebMethodActionHandler(IUserInfoAppService userInfoAppService,IRoleAppService roleAppService)
        {
            _userInfoAppService = userInfoAppService;
            _roleAppService = roleAppService;
        }

        // Check whether a given MinimumAgeRequirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WebMethodActionRequirement requirement)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                var userInfo = _userInfoAppService.GetUserInfo(long.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.PrimarySid).Value));
                if (userInfo.UserType == (int)UserTypeEnum.超级管理员)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var roleIDS = userInfo.Roles?.Select(x => x.RoleId).ToList();
                    List<long> moduleIDS = new List<long>();
                    foreach (var v in roleIDS)
                    {
                        moduleIDS.AddRange(_roleAppService.GetRoleInfo(v)?.Module.Select(x=>x.ModuleID));
                    }
                    //roleIDS?.ForEach(x => moduleIDS.AddRange(_roleAppService.GetRoleInfo(x).Menus.Select(a=>a.MenuInfo.Modules).ToList()));
                    moduleIDS.Distinct().ToList();
                    if (moduleIDS.Contains((long)requirement.ModulesType))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
