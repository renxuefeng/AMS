using AMS.Models.Dto;
using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AMS.BLL.UserApp
{
    public interface IUserInfoAppService
    {
        /// <summary>
        /// 获取所有用户数据
        /// </summary>
        /// <returns></returns>

        List<UserInfo> GetUserList();
        /// <summary>
        /// 检查用户密码是否正确
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserInfo CheckUserPassword(string user, string password);

        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <returns></returns>
        UserInfo GetUserInfoByUserName(string userName);

        List<UserInfo> GetUserList(Expression<Func<UserInfo, bool>> where);
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<UserInfo> GetUserList(int startPage, int pageSize, out int rowCount, out int pageCount);
        /// <summary>
        /// 获取用户分页数据（带条件）
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<UserInfo> GetUserList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<UserInfo, bool>> where);
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserInfo CheckUser(string userName);

        /// <summary>
        /// 更新或新建用户
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        UserInfo InsertOrUpdate(UserInfo ui);
        /// <summary>
        /// 编辑用户基础信息（不包含密码、角色）
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        UserInfo InsertOrUpdateBase(UserInfo ui);


        UserInfo InsertOrUpdate(UserInfo ri, List<long> roleIDS);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>

        bool Delete(Int64 id);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetUserInfo(Int64 id);

        /// <summary>
        /// 根据角色获取所有用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<UserInfo> GetRoleInUser(Int64 id);
        /// <summary>
        /// 根据角色名称获取所有用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<UserInfo> GetRoleInUser(string roleName);
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        UserInfo ResetPassword(ResetPasswordDto userInfo, bool self);
    }
}
