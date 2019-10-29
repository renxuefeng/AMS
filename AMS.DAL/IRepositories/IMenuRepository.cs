using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.DAL.IRepositories
{
    public interface IMenuRepository : IRepository<MenuInfo>
    {
        /// <summary>
        /// 获取菜单列表（含角色信息）
        /// </summary>
        /// <returns></returns>
        List<MenuInfo> GetAllListIncludeModules();
    }
}
