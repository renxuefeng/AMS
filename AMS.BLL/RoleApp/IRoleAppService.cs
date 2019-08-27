using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AMS.BLL.RoleApp
{
    /**********************************************************************************************//**
     * @interface   IRoleAppService
     *
     * @brief   角色依赖注入接口
     *
     * @author  rxf
     * @date    2017/12/27
     **************************************************************************************************/

    public interface IRoleAppService
    {
        /**********************************************************************************************//**
         * @fn  List<RoleInfo> GetAllList();
         *
         * @brief   获取列表
         *
         * @return  all list.
         **************************************************************************************************/

        List<RoleInfo> GetAllList();

        /**********************************************************************************************//**
         * @fn  List<RoleInfo> GetAllList();
         *
         * @brief   获取列表
         *
         * @return  all list.
         **************************************************************************************************/

        List<RoleInfo> GetAllList(Expression<Func<RoleInfo, bool>> where);
        /**********************************************************************************************//**
         * @fn  List<RoleInfo> GetAllPageList(int startPage, int pageSize, out int rowCount);
         *
         * @brief   获取分页列表
         *
         * @param       startPage   起始页.
         * @param       pageSize    页面大小.
         * @param [out] rowCount    数据总数.
         * @param [out] pageCount   页面总数.
         *
         * @return  all page list.
         **************************************************************************************************/

        List<RoleInfo> GetAllPageList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<RoleInfo, bool>> where);

        /**********************************************************************************************//**
         * @fn  bool InsertOrUpdate(RoleInfo dto);
         *
         * @brief   新增或修改角色信息
         *
         * @param   dto 实体.
         *
         * @return  True if it succeeds, false if it fails.
         **************************************************************************************************/

        bool InsertOrUpdate(RoleInfo dto);

        /**********************************************************************************************//**
         * @fn  bool InsertOrUpdate(RoleInfo ri, List<long> menuList, List<long> moudel);
         *
         * @brief   新增或修改角色信息、功能、权限信息
         *
         * @param   ri          The ri.
         * @param   menuList    List of menus.
         * @param   moudel      The moudel.
         *
         * @return  True if it succeeds, false if it fails.
         **************************************************************************************************/

        bool InsertOrUpdate(RoleInfo ri, List<long> menuList, List<long> moudel);



        /**********************************************************************************************//**
         * @fn  void Delete(Int64 id);
         *
         * @brief   删除
         *
         * @param   id  Id.
         **************************************************************************************************/

        bool Delete(Int64 id);

        /**********************************************************************************************//**
         * @fn  RoleInfo Get(Int64 id);
         *
         * @brief   根据Id获取实体
         *
         * @param   id  Id.
         *
         * @return  A RoleInfo.
         **************************************************************************************************/

        RoleInfo Get(Int64 id);

        /**********************************************************************************************//**
         * @fn  List<RoleInfo> GetRoleInfo(Int64 id);
         *
         * @brief   获取角色信息
         *
         * @param   id  角色ID.
         *
         * @return  The role information.
         **************************************************************************************************/

        RoleInfo GetRoleInfo(Int64 id);

        /**********************************************************************************************//**
         * @fn  GetRoleInfo(string roleName);
         *
         * @brief   获取角色信息
         *
         * @param   roleName  角色名称.
         *
         * @return  The role information.
         **************************************************************************************************/

        RoleInfo GetRoleInfo(string roleName);
        /**********************************************************************************************//**
         * @fn  string GetRoleStatus(Int64 id);
         *
         * @brief   Gets role status
         *
         * @param   id  The identifier.
         *
         * @return  The role status.
         **************************************************************************************************/

        string GetRoleStatus(Int64 id);
    }
}
