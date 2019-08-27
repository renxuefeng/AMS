using AMS.DAL.IRepositories;
using AMS.Models.Dto;
using AMS.Models.Entitys;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.BLL.UserApp
{
    public class UserInfoAppService : IUserInfoAppService
    {

        private readonly IUserRepository _repository;

        public UserInfoAppService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }
        public List<UserInfo> GetUserList()
        {
            return _repository.GetAllList();
        }

        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <returns></returns>
        public UserInfo GetUserInfoByUserName(string userName)
        {
            return _repository.GetUserInfoByUserName(userName);
        }

        public List<UserInfo> GetUserList(Expression<Func<UserInfo, bool>> where)
        {
            return _repository.GetAllList(where);
        }
        public List<UserInfo> GetUserList(int startPage, int pageSize, out int rowCount, out int pageCount)
        {
            return _repository.LoadPageList(startPage, pageSize, out rowCount, out pageCount, null, null).ToList();
        }
        public List<UserInfo> GetUserList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<UserInfo, bool>> where)
        {
            rowCount = 0;
            pageCount = 0;

            return _repository.LoadPageList(startPage, pageSize, out rowCount, out pageCount, where, null, it => it.CreateUserTime).ToList();
        }
        /// <summary>
        /// 检测用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserInfo CheckUser(string userName)
        {
            return _repository.CheckUser(userName);
        }

        /// <summary>
        /// 新增 or 编辑
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public UserInfo InsertOrUpdate(UserInfo ui)
        {
            var user = _repository.InsertOrUpdate(ui);
            return user;
        }
        public UserInfo InsertOrUpdateBase(UserInfo ui)
        {
            UserInfo userInfo = _repository.GetUserInfo(ui.Id);
            userInfo.Name = ui.Name;
            userInfo.Status = ui.Status;
            userInfo.Gender = ui.Gender;
            userInfo.UserPic = ui.UserPic;
            userInfo.WorkUnit = ui.WorkUnit;
            var user = _repository.InsertOrUpdate(userInfo);
            return user;
        }
        /// <summary>
        /// 删除用户(含角色对应关系)
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(Int64 id)
        {
           return _repository.DeleteUserInfo(id);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(long id)
        {
            return _repository.GetUserInfo(id);
        }


        public List<UserInfo> GetRoleInUser(long id)
        {
            return _repository.GetRoleInUser(id);
        }

        public List<UserInfo> GetRoleInUser(string roleName)
        {
            return _repository.GetRoleInUser(roleName);
        }


        /// <summary>
        /// 新增或编辑用户信息
        /// </summary>
        /// <param name="ri"></param>
        /// <param name="roleIDS"></param>
        /// <returns></returns>
        public UserInfo InsertOrUpdate(UserInfo ri, List<long> roleIDS)
        {
            ri.Roles = new List<UserInRole>();
            roleIDS.ForEach(x => ri.Roles.Add(new UserInRole() { UserId = ri.Id, RoleId = x }));
            UserInfo ui = _repository.InsertOrUpdate(ri);
            return ui;
        }

        public UserInfo CheckUserPassword(string user, string password)
        {
            return _repository.CheckUserPassword(user, password);
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfo ResetPassword(ResetPasswordDto userInfo, bool self)
        {
            UserInfo ui = _repository.GetUserInfo(userInfo.UserId);
            if (self)
                ui = _repository.CheckUserPassword(ui.UserName, userInfo.OldPassword);
            if (ui != null)
            {
                ui.Password = userInfo.NewPasswrod;
                return _repository.Update(ui);
            }
            else
            {
                return null;
            }
        }
    }
}
