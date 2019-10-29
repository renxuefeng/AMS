using AMS.DAL.IRepositories;
using AMS.Models.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DAL.Repositories
{
    public class MenuRepository : RepositoryBase<MenuInfo>, IMenuRepository
    {
        public MenuRepository(AMSDbContext dbcontext) : base(dbcontext)
        {

        }
        /// <summary>
        /// 获取菜单信息（含权限）
        /// </summary>
        /// <returns></returns>
        public List<MenuInfo> GetAllListIncludeModules()
        {
            List<MenuInfo> menuInfos = _dbContext.Set<MenuInfo>().Include(a => a.Modules).ToList();
            menuInfos.ForEach(x => x.Down["Modules"] = x.Modules.Select(a => a.ModuleID).ToArray());
            return menuInfos;
        }
    }
}
