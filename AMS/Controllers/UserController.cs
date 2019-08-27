﻿using System;
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
                    userInfo.Roles = new List<UserInRole>();
                    List<long> roleIDS = userInfo.Roles?.Select(x => x.Id)?.ToList();
                    roleIDS.ForEach(x => userInfo.Roles?.Add(new UserInRole() { RoleId = x }));
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
        public ActionResult<ResponseData> List()
        {
            _responseData.Success = true;
            _responseData.Data = _userInfoAppService.GetUserList();
            return _responseData;
        }
        /// <summary>
        /// 获取用户列表
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
    }
}
