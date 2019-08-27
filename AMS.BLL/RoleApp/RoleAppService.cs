using AMS.DAL.IRepositories;
using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.BLL.RoleApp
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRoleRepository _repository;
        public RoleAppService(IRoleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleInfo GetRoleInfo(long id)
        {
            return _repository.GetRoleInfo(id);
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public RoleInfo GetRoleInfo(string roleName)
        {
            return _repository.GetRoleInfo(roleName);
        }
        /// <summary>
        /// 获取角色状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public string GetRoleStatus(long id)
        {
            return _repository.GetRoleStatus(id);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<RoleInfo> GetAllList()
        {
            return _repository.GetAllList().OrderBy(it => it.RoleName).ToList();
        }

        /// <summary>
        /// 获取列表(条件)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<RoleInfo> GetAllList(Expression<Func<RoleInfo, bool>> where)
        {
            return _repository.GetAllList(where).OrderBy(it => it.RoleName).ToList();
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<RoleInfo> GetAllPageList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<RoleInfo, bool>> where)
        {
            return _repository.LoadPageList(startPage, pageSize, out rowCount, out pageCount, where, null, it => it.CreateDateTime).ToList();
        }

        /// <summary>
        /// 重载新增或修改
        /// </summary>
        /// <param name="ri"></param>
        /// <returns></returns>
        public bool InsertOrUpdate(RoleInfo ri)
        {
            return InsertOrUpdate(ri, null, null);
        }
        /// <summary>
        /// 重载新增或修改
        /// </summary>
        /// <param name="ri"></param>
        /// <param name="menuList"></param>
        /// <param name="moudel"></param>
        /// <returns></returns>

        public bool InsertOrUpdate(RoleInfo ri, List<long> menuList, List<long> moudel)
        {
            ri.Modules = new List<RoleInModule>();
            ri.Menus = new List<RoleInMenu>();
            moudel?.ForEach(x => ri.Modules.Add(new RoleInModule() { RoleId = ri.Id, ModuleID = x }));
            menuList?.ForEach(x => ri.Menus.Add(new RoleInMenu() { RoleId = ri.Id, MenuId = x }));
            var roleInfo = _repository.InsertOrUpdate(ri);
            return roleInfo == null ? false : true;
        }

        /// <summary>
        /// 删除角色及关联信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool Delete(Int64 id)
        {
            return _repository.DeleteRoleInfo(id);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleInfo Get(Int64 id)
        {
            return _repository.Get(id);
        }
    }
}
