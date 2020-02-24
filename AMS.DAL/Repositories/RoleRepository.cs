using AMS.Common;
using AMS.DAL.IRepositories;
using AMS.Models.Entitys;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.DAL.Repositories
{

    public class RoleRepository : RepositoryBase<RoleInfo>, IRoleRepository
    {
        public RoleRepository(AMSDbContext dbcontext) : base(dbcontext)
        {

        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleInfo GetRoleInfo(Int64 id)
        {
            RoleInfo ri = _dbContext.Set<RoleInfo>().Where(it => it.Id == id)
                .Include(o => o.Menus)
                    .ThenInclude(a => a.MenuInfo)
                        .ThenInclude(b => b.Modules)
                .Include(c => c.Module)
                .FirstOrDefault();
            List<long> idsModules = new List<long>();
            List<string> nameModules = new List<string>();
            if (ri.Menus != null && ri.Menus.Count >0)
            {
                ri.Menus.ForEach(x =>
                {
                    idsModules.AddRange(x.MenuInfo.Modules.Select(a => a.ModuleID));
                    nameModules.AddRange(x.MenuInfo.Modules.Select(a => ((ModulesType)a.ModuleID).ToString()));
                });
            }
            else if (ri.Module != null && ri.Module.Count > 0)
            {
                idsModules = ri.Module.Select(y => y.ModuleID).ToList();
                nameModules = ri.Module.Select(a => ((ModulesType)a.ModuleID).ToString()).ToList();
            }
            ri.Down["modules"] = idsModules;
            ri.Down["modulesName"] = string.Join(',', nameModules);
            return ri;
        }

        /// <summary>
        /// 获取角色状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetRoleStatus(Int64 id)
        {
            var v = _dbContext.Set<UserInRole>().Where(it => it.RoleId == id);
            if (v.Count() > 0)
            {
                return "使用中";
            }
            else
            {
                return "无";
            }
        }
        /// <summary>
        /// 根据角色名获取角色信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public RoleInfo GetRoleInfo(string roleName)
        {
            RoleInfo ri = _dbContext.Set<RoleInfo>().Where(it => it.RoleName == roleName).FirstOrDefault();
            return ri;
        }

        /// <summary>
        /// 根据角色ID删除角色及关联信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool DeleteRoleInfo(long roleID)
        {
            RoleInfo ri = _dbContext.Set<RoleInfo>().Where(it => it.Id == roleID).FirstOrDefault();
            _dbContext.Set<RoleInfo>().Remove(ri);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
