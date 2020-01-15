using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.DAL.IRepositories
{
    /**********************************************************************************************//**
     * @interface   IUserRepository
     *
     * @brief   用户管理仓储接口
     *
     * @author  rxf
     * @date    2017/12/25
     **************************************************************************************************/

    public interface IUserRepository : IRepository<UserInfo>
    {
        /// <summary>
        /// 接口成员检查用户是存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserInfo CheckUser(string userName);
        /// <summary>
        /// 检查用户名及密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserInfo CheckUserPassword(string userName,string password);

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetUserInfo(Int64 id);
        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <returns></returns>
        UserInfo GetUserInfoByUserName(string userName);


        List<UserInfo> GetRoleInUser(Int64 id);

        List<UserInfo> GetRoleInUser(string roleName);


        bool DeleteUserInfo(long userId);

        IQueryable<UserInfo> GetUserQueryable();
        /// <summary>
        /// 获取用户分页列表（含角色信息）
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="orderTime"></param>
        /// <returns></returns>
        IQueryable<UserInfo> UserListIncludeRole(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<UserInfo, bool>> where = null, Expression<Func<UserInfo, object>> order = null, Expression<Func<UserInfo, DateTime>> orderTime = null);
    }
}
