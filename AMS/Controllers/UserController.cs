using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AMS.Authorization;
using AMS.BLL.RoleApp;
using AMS.BLL.UserApp;
using AMS.Models;
using AMS.Models.Dto;
using AMS.Models.Entitys;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly ResponseData _responseData;
        public UserController(IUserInfoAppService userInfoAppService, IRoleAppService roleAppService, ResponseData responseData, ILogger<UserController> logger):base(logger)
        {
            _userInfoAppService = userInfoAppService;
            _roleAppService = roleAppService;
            _responseData = responseData;
        }
        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="userInfo"></param>
        // POST api/<controller>
        [WebMethodAction(ModulesType.新建用户信息)]
        [HttpPost]
        public ActionResult<ResponseData> Create(UserInfo userInfo)
        {
            if (!string.IsNullOrWhiteSpace(userInfo?.UserName))
            {
                if (_userInfoAppService.CheckUser(userInfo.UserName) != null)
                {
                    _responseData.Success = false;
                    _responseData.Message = "用户已存在";
                }
                else
                {
                    List<long> roleIDS = JsonConvert.DeserializeObject<List<long>>(userInfo.Up["roleIDS"].ToString());
                    if (roleIDS != null && roleIDS.Count > 0)
                    {
                        userInfo.Roles = new List<UserInRole>();
                        roleIDS?.ForEach(x => userInfo.Roles?.Add(new UserInRole() { RoleId = x }));
                    }
                    userInfo.CreateUserID = long.Parse(User.FindFirst(x => x.Type == ClaimTypes.PrimarySid).Value);
                    userInfo.CreateUserTime = DateTime.Now;
                    var user = _userInfoAppService.InsertOrUpdate(userInfo);
                    _responseData.Success = true;
                    _responseData.Data = user;
                }
            }
            return _responseData;
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [WebMethodAction(ModulesType.编辑用户信息)]
        [HttpPut]
        public ActionResult<ResponseData> Update(UserInfo userInfo)
        {
            var info = _userInfoAppService.GetUserInfo(userInfo.Id);
            if (info != null)
            {
                // 根据需要添加
                info.Status = userInfo.Status;
                info.UserType = userInfo.UserType;
                info.Name = userInfo.Name;
                info.UserName = userInfo.UserName;
                List<long> roleIDS = userInfo.Roles?.Select(x => x.Id)?.ToList();
                info.Roles = new List<UserInRole>();
                roleIDS.ForEach(x => info.Roles.Add(new UserInRole() { UserId = info.Id, RoleId = x }));
                var user = _userInfoAppService.InsertOrUpdate(info);
                _responseData.Success = true;
                _responseData.Data = user;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户不存在";
            }
            return _responseData;
        }
        /// <summary>
        /// 编辑当前登录用户基础信息（不含密码、角色）
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [WebMethodAction(ModulesType.编辑用户基础信息)]
        [HttpPut("UpdateBasic")]
        public ActionResult<ResponseData> UpdateBasic(UserInfo userInfo)
        {
            var info = _userInfoAppService.GetUserInfo(long.Parse(User.FindFirst(x => x.Type == ClaimTypes.PrimarySid).Value));
            if (info != null)
            {
                info.Name = userInfo.Name;
                info.UserPic = userInfo.UserPic;
                info.WorkUnit = userInfo.WorkUnit;
                var user = _userInfoAppService.InsertOrUpdate(info);
                _responseData.Success = true;
                _responseData.Data = user;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户不存在";
            }
            return _responseData;
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethodAction(ModulesType.删除用户信息)]
        [HttpDelete("{id}")]
        public ActionResult<ResponseData> Delete(long id)
        {
            var info = _userInfoAppService.GetUserInfo(id);
            if (info != null)
            {
                _responseData.Success = _userInfoAppService.Delete(info.Id);
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户不存在";
            }
            return _responseData;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        [WebMethodAction(ModulesType.修改密码)]
        [HttpPut("ResetPassword")]
        public ActionResult<ResponseData> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var info = _userInfoAppService.GetUserInfo(resetPasswordDto.UserId);
            if (info != null)
            {
                var user = _userInfoAppService.ResetPassword(resetPasswordDto, false);
                _responseData.Success = true;
                _responseData.Data = user;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户不存在";
            }
            return _responseData;
        }
        /// <summary>
        /// 修改我的密码
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        [WebMethodAction(ModulesType.修改我的密码)]
        [HttpPut("ResetSelfPassword")]
        public ActionResult<ResponseData> ResetSelfPassword(ResetPasswordDto resetPasswordDto)
        {
            var info = _userInfoAppService.GetUserInfo(UserId);
            if (info != null)
            {
                resetPasswordDto.UserId = info.Id;
                var user = _userInfoAppService.ResetPassword(resetPasswordDto, true);
                if (user != null)
                {
                    _responseData.Success = true;
                    _responseData.Data = user;
                }
                else
                {
                    _responseData.Success = false;
                    _responseData.Message = "检查原始密码是否正确";
                }
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户不存在";
            }
            return _responseData;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.获取用户列表)]
        [HttpGet]
        public ActionResult<ResponseData> List(int startPage,int pageSize)
        {
            int count = 0;
            int pagecount = 0;
            _responseData.Success = true;
            PageInfo pageInfo = new PageInfo();
            List<UserInfo> uiList = _userInfoAppService.GetUserList(startPage, pageSize, out count, out pagecount);
            // 取用户角色名称下发前台
            uiList.ForEach(x => {
                x.Down["roleName"] = string.Join(',', x.Roles.Select(y => y.RoleInfo?.RoleName) );
            });
            pageInfo.data = uiList;
            pageInfo.rowCount = count;
            pageInfo.pageCount = pagecount;
            _responseData.Data = pageInfo;
            return _responseData;
        }
        /// <summary>
        /// 获取已登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public ActionResult<ResponseData> Get()
        {
            _responseData.Success = true;
            if (UserId > 0)
            {
                UserInfo userInfo = _userInfoAppService.GetUserInfo(UserId);
                if (userInfo.UserType == (int)UserTypeEnum.超级管理员)
                {
                    Dictionary<int, string> pList = new Dictionary<int, string>();
                    foreach (ModulesType foo in Enum.GetValues(typeof(ModulesType)))
                    {
                        pList.Add((int)foo, foo.ToString());
                    }
                    userInfo.Down["Modules"] = pList.Select(x => x.Key).ToArray();
                }
                else
                {
                    userInfo.Down["Modules"] = userInfo.Roles?.Select(x => x.RoleInfo).Select(y => y.Module)?.FirstOrDefault().ToArray().Select(x => x.ModuleID);
                    //userInfo.Down["Modules"] = userInfo.Roles?.Select(x => x.RoleInfo).Select(y => y.Modules?.Select(z => z.ModuleID))?.FirstOrDefault().ToArray();
                }
                _responseData.Data = userInfo;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户过期，请重新登录";
            }
            return _responseData;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.获取用户信息)]
        [HttpGet("{id}")]
        public ActionResult<ResponseData> Get(long id)
        {
            _responseData.Success = true;
            _responseData.Data = _userInfoAppService.GetUserInfo(id);
            return _responseData;
        }
        /// <summary>
        /// 获取用户类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserType")]
        public ActionResult<ResponseData> GetUserType()
        {
            Dictionary<int, string> pList = new Dictionary<int, string>();
            foreach (UserTypeEnum foo in Enum.GetValues(typeof(UserTypeEnum)))
            {
                pList.Add((int)foo, foo.ToString());
            }
            _responseData.Success = true;
            _responseData.Data = pList.ToArray();
            return _responseData;
        }
        [HttpPost("Logout")]
        public ActionResult<ResponseData> Logout()
        {
            _responseData.Success = true;
            return _responseData;
        }
    }
}
