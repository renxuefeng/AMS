using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AMS.BLL.UserApp;
using AMS.Common.Configuration;
using AMS.Helpers;
using AMS.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly ResponseData _responseData;
        private readonly AudienceConfiguration _audienceConfiguration;
        private readonly IStringLocalizer<LoginController> _stringLocalizer;
        public LoginController(IUserInfoAppService userInfoAppService, ResponseData responseData, AudienceConfiguration audienceConfiguration, IStringLocalizer<LoginController> stringLocalizer, ILogger<LoginController> logger) : base(logger)
        {
            _userInfoAppService = userInfoAppService;
            _responseData = responseData;
            _audienceConfiguration = audienceConfiguration;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResponseData> Login(LoginInfo loginInfo)
        {
            string jwtStr = string.Empty;

            var user = _userInfoAppService.CheckUserPassword(loginInfo.userName, loginInfo.password);
            if (user != null)
            {
                var userRoles = user.Roles?.Select(x => x.RoleId);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_audienceConfiguration.Expiration).ToString())
                };
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                var token = JWTHelper.BuildJwtToken(claims.ToArray(), _audienceConfiguration);
                _responseData.Success = true;
                _responseData.Data = token;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = _stringLocalizer["ErrorMsg"];
            }
            return _responseData;
        }
    }
}
