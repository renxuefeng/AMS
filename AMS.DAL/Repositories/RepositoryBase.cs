using AMS.DAL.IRepositories;
using AMS.Models;
using AMS.Models.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.DAL.Repositories
{
    /**********************************************************************************************//**
     * @class   RepositoryBase<TEntity,TPrimaryKey>
     *
     * @brief   仓储基类
     *
     * @author  rxf
     * @date    2017/12/26
     *
     * @tparam  TEntity     Type of the entity.
     * @tparam  TPrimaryKey Type of the primary key.
     **************************************************************************************************/

    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {

        /** @brief   定义数据访问上下文对象 */
        protected readonly AMSDbContext _dbContext;

        /**********************************************************************************************//**
         * @fn  public RepositoryBase(FCDbContext dbContext)
         *
         * @brief   通过构造函数注入得到数据上下文对象实例
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   dbContext   Context for the database.
         **************************************************************************************************/

        public RepositoryBase(AMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /**********************************************************************************************//**
         * @fn  public List<TEntity> GetAllList()
         *
         * @brief   获取实体集合
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @return  all list.
         **************************************************************************************************/

        public List<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        /**********************************************************************************************//**
         * @fn  public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
         *
         * @brief   根据lambda表达式条件获取实体集合
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   predicate   The predicate.
         *
         * @return  all list.
         **************************************************************************************************/

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        /**********************************************************************************************//**
         * @fn  public TEntity Get(TPrimaryKey id)
         *
         * @brief   根据主键获取实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   id  The Identifier to get.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        public TEntity Get(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /**********************************************************************************************//**
         * @fn  public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
         *
         * @brief   根据lambda表达式条件获取单个实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   predicate   lambda表达式条件.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /**********************************************************************************************//**
         * @fn  public TEntity Insert(TEntity entity, bool autoSave = true)
         *
         * @brief   新增实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   entity      实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        public TEntity Insert(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Add(entity);
            if (autoSave)
                Save();
            return entity;
        }

        /**********************************************************************************************//**
         * @fn  public TEntity Update(TEntity entity, bool autoSave = true)
         *
         * @brief   更新实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   entity      实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        public TEntity Update(TEntity entity, bool autoSave = true)
        {
            var obj = Get(entity.Id);
            EntityToEntity(entity, obj);
            if (autoSave)
                Save();
            return entity;
        }
        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var mItem in typeof(T).GetProperties())
            {
                object[] o = mItem.GetCustomAttributes(typeof(NotMappedAttribute), true);
                if (o.Count() == 0)
                {
                    //if (mItem.Name != "down" && mItem.Name != "up"&& mItem.Name != "Down" && mItem.Name != "Up")
                    mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
                }
            }
        }

        /**********************************************************************************************//**
         * @fn  public TEntity InsertOrUpdate(TEntity entity, bool autoSave = true)
         *
         * @brief   新增或更新实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   entity      实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         *
         * @return  A TEntity.
         **************************************************************************************************/

        public TEntity InsertOrUpdate(TEntity entity, bool autoSave = true)
        {
            if (Get(entity.Id) != null)
                return Update(entity, autoSave);
            return Insert(entity, autoSave);
        }

        /**********************************************************************************************//**
         * @fn  public void Delete(TEntity entity, bool autoSave = true)
         *
         * @brief   删除实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   entity      要删除的实体.
         * @param   autoSave    (Optional) 是否立即执行保存.
         **************************************************************************************************/

        public void Delete(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
                Save();
        }

        /**********************************************************************************************//**
         * @fn  public void Delete(TPrimaryKey id, bool autoSave = true)
         *
         * @brief   删除实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   id          实体主键.
         * @param   autoSave    (Optional) 是否立即执行保存.
         **************************************************************************************************/

        public void Delete(TPrimaryKey id, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(Get(id));
            if (autoSave)
                Save();
        }

        /**********************************************************************************************//**
         * @fn  public void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true)
         *
         * @brief   根据条件删除实体
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   where       lambda表达式.
         * @param   autoSave    (Optional) 是否自动保存.
         **************************************************************************************************/

        public void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it => _dbContext.Set<TEntity>().Remove(it));
            if (autoSave)
                Save();
        }

        public void Delete(Expression<Func<TEntity, bool>> where, bool isAll, bool autoSave = true)
        {
            if (isAll)
            {
                _dbContext.Set<TEntity>().Where(where).ToList().ForEach(x => _dbContext.Set<TEntity>().Remove(x));
                if (autoSave)
                    Save();
            }
        }
        /**********************************************************************************************//**
         * @fn  public IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
         *
         * @brief   分页查询
         *
         * @author  rxf
         * @date    2017/1/3
         *
         * @param       startPage   页码.
         * @param       pageSize    单页数据数.
         * @param [out] rowCount    行数.
         * @param [out] pageCount   页数.
         * @param       where       (Optional) 条件.
         * @param       order       (Optional) 排序.
         *
         * @return  The page list.
         **************************************************************************************************/

        public IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var result = from p in _dbContext.Set<TEntity>()
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            rowCount = result.Count();
            pageCount = (int)rowCount / pageSize;
            if (rowCount % pageSize != 0)
            {
                //如果余数不为0总页数就加上1
                pageCount = pageCount + 1;
            }
            // 请求的起始页大于总页数则取最后一页数据
            if (pageCount > 0 && startPage > pageCount)
            {
                startPage = pageCount;
            }
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }
        public IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, Expression<Func<TEntity, DateTime>> orderTime = null)
        {
            var result = from p in _dbContext.Set<TEntity>()
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else if (orderTime != null)
                result = result.OrderByDescending(orderTime);
            else
                result = result.OrderBy(m => m.Id);
            rowCount = result.Count();
            pageCount = (int)rowCount / pageSize;
            if (rowCount % pageSize != 0)
            {
                //如果余数不为0总页数就加上1
                pageCount = pageCount + 1;
            }
            // 请求的起始页大于总页数则取最后一页数据
            if (pageCount > 0 && startPage > pageCount)
            {
                startPage = pageCount;
            }
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }

        //public List<RoleInfo> GetAllRoleInfo(TEntity t)
        //{
        //    if (t != null)
        //    {
        //        if (t is OperationLog)
        //        {
        //            var v = from a in _dbContext.Set<OperationLog>()
        //                    join b in _dbContext.Set<UserInRole>() on a.Id equals b.UserID  /* into ab*/
        //                    //from b in ab.DefaultIfEmpty()
        //                    join c in _dbContext.Set<RoleInfo>() on b.RoleID equals c.Id /*into bc*/
        //                    //from c in bc.DefaultIfEmpty()
        //                    select c;
        //            v = v.Where(x => x.Id > 0);
        //            return v.ToList();
        //        }
        //    }
        //    return new List<RoleInfo>();
        //}
        /**********************************************************************************************//**
         * @fn  public void Save()
         *
         * @brief   事务性保存
         *
         * @author  rxf
         * @date    2017/12/26
         **************************************************************************************************/

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                //LogProviderHelper.Instance.WriteLog(e.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                throw new Exception("数据操作失败");
            }
        }

        /**********************************************************************************************//**
         * @fn  protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
         *
         * @brief   根据主键构建判断表达式
         *
         * @author  rxf
         * @date    2017/12/26
         *
         * @param   id  主键.
         *
         * @return  The new equality expression for identifier.
         **************************************************************************************************/

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        List<RoleInfo> IRepository<TEntity, TPrimaryKey>.GetAllRoleInfo(TEntity t)
        {
            throw new NotImplementedException();
        }
        //protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(List<long> ids, TPrimaryKey id)
        //{
        //    ParameterExpression param = Expression.Parameter(typeof(TEntity),"x");
        //    ConstantExpression value = Expression.Constant(ids, typeof(List<long>));
        //    BinaryExpression body = Expression.Constant(param);

        //    return Expression.Lambda<Func<TEntity, bool>>(body, param);
        //}
    }

    /**********************************************************************************************//**
     * @class   MmPSRepositoryBase<TEntity>
     *
     * @brief   主键仓储基类
     *
     * @author  rxf
     * @date    2017/12/26
     *
     * @tparam  TEntity Type of the entity.
     *
     * ### tparam   TEntity 实体类型.
     **************************************************************************************************/

    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, Int64> where TEntity : Entity
    {
        public RepositoryBase(AMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
