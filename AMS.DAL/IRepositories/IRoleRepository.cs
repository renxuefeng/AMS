using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.DAL.IRepositories
{

    public interface IRoleRepository : IRepository<RoleInfo>
    {
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleInfo GetRoleInfo(long id);
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        RoleInfo GetRoleInfo(string roleName);
        /// <summary>
        /// 获取角色状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetRoleStatus(long id);
        /// <summary>
        /// 删除角色及关联信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        bool DeleteRoleInfo(long roleID);
    }
}
