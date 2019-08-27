using AMS.Models;
using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.DAL.IRepositories
{
    /**********************************************************************************************//**
     * @interface   IRepository
     *
     * @brief   仓储接口定义
     *
     * @author  rxf
     * @date    2017/12/26
     **************************************************************************************************/

    public interface IRepository
    {

    }

    /**********************************************************************************************//**
     * @interface   IRepository<TEntity,TPrimaryKey>
     *
     * @brief   定义泛型仓储接口
     *
     * @author  rxf
     * @date    2017/12/26
     *
     * @tparam  TEntity     Type of the entity.
     * @tparam  TPrimaryKey Type of the primary key.
     *
     * ### tparam   TEntity     实体类型.
     * ### tparam   TPrimaryKey 主键类型.
     **************************************************************************************************/

    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        /**********************************************************************************************//**
         * @fn  List<TEntity> GetAllList();
         *
         * @brief   获取实体集合
         *
         * @return  all list.
         **************************************************************************************************/

        List<TEntity> GetAllList();

        /**********************************************************************************************//**
         * @fn  List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
         *
         * @brief   根据lambda表达式条件获取实体集合
         *
         * @param   predicate   lambda表达式条件.
         *
         * @return  all list.
         **************************************************************************************************/

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /**********************************************************************************************//**
         * @fn  TEntity Get(TPrimaryKey id);
         *
         * @brief   根据主键获取实体
         *
         * @param   id  实体主键.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        TEntity Get(TPrimaryKey id);

        /**********************************************************************************************//**
         * @fn  TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
         *
         * @brief   根据lambda表达式条件获取单个实体
         *
         * @param   predicate   lambda表达式条件.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /**********************************************************************************************//**
         * @fn  TEntity Insert(TEntity entity, bool autoSave = true);
         *
         * @brief   新增实体
         *
         * @param   entity      实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        TEntity Insert(TEntity entity, bool autoSave = true);

        /**********************************************************************************************//**
         * @fn  TEntity Update(TEntity entity, bool autoSave = true);
         *
         * @brief   更新实体
         *
         * @param   entity      实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        TEntity Update(TEntity entity, bool autoSave = true);

        /**********************************************************************************************//**
         * @fn  TEntity InsertOrUpdate(TEntity entity, bool autoSave = true);
         *
         * @brief   新增或更新实体
         *
         * @param   entity      实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        TEntity InsertOrUpdate(TEntity entity, bool autoSave = true);

        /**********************************************************************************************//**
         * @fn  void Delete(TEntity entity, bool autoSave = true);
         *
         * @brief   删除实体
         *
         * @param   entity      要删除的实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         **************************************************************************************************/

        void Delete(TEntity entity, bool autoSave = true);

        /**********************************************************************************************//**
         * @fn  void Delete(TPrimaryKey id, bool autoSave = true);
         *
         * @brief   删除实体
         *
         * @param   id          实体主键.
         * @param   autoSave    (Optional) 是否立即执行保存.
         **************************************************************************************************/

        void Delete(TPrimaryKey id, bool autoSave = true);

        /**********************************************************************************************//**
         * @fn  void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true);
         *
         * @brief   根据条件删除实体
         *
         * @param   where       lambda表达式.
         * @param   autoSave    (Optional) 是否自动保存.
         **************************************************************************************************/

        void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true);

        /**********************************************************************************************//**
         * @fn  void Delete(bool isAll, bool autoSave = true);
         *
         * @brief   清空全部数据
         *
         * @param   isAll       True if this object is all.
         * @param   autoSave    (Optional) True to automatically save.
         **************************************************************************************************/

        void Delete(Expression<Func<TEntity, bool>> where, bool isAll, bool autoSave = true);
        /**********************************************************************************************//**
         * @fn  IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order);
         *
         * @brief   分页获取数据
         *
         * @param       startPage   起始页.
         * @param       pageSize    页面条目.
         * @param [out] rowCount    数据总数.
         * @param       where       查询条件.
         * @param       order       排序.
         *
         * @return  The page list.
         **************************************************************************************************/

        IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order);
        IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order, Expression<Func<TEntity, DateTime>> orderTime);

        List<RoleInfo> GetAllRoleInfo(TEntity t);

        void Save();

    }

    /**********************************************************************************************//**
     * @interface   IRepository<TEntity>
     *
     * @brief   主键类型仓储
     *
     * @author  rxf
     * @date    2017/12/26
     *
     * @tparam  TEntity Type of the entity.
     *
     * ### tparam   TEntity .
     **************************************************************************************************/

    public interface IRepository<TEntity> : IRepository<TEntity, Int64> where TEntity : Entity
    {

    }
}
