﻿using AMS.DAL.IRepositories;
using AMS.Models.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AMS.DAL.Repositories
{
    public class UserRepository : RepositoryBase<UserInfo>, IUserRepository
    {
        public UserRepository(AMSDbContext dbcontext) : base(dbcontext)
        {

        }


        /// <summary>
        /// 检查用户是存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        public UserInfo CheckUser(string userName)
        {
            return _dbContext.Set<UserInfo>().FirstOrDefault(it => it.UserName == userName);
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public UserInfo GetUserInfo(Int64 id)
        {
            var user = _dbContext.Set<UserInfo>()
                .Include(x => x.Roles)
                    .ThenInclude(x => x.RoleInfo)
                        .ThenInclude(x => x.Menus)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.RoleInfo)
                        .ThenInclude(x=>x.Module)
                .FirstOrDefault(it => it.Id == id);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <returns></returns>
        public UserInfo GetUserInfoByUserName(string userName)
        {
            return _dbContext.Set<UserInfo>().Where(u => u.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        /// 根据角色ID获取使用该角色的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserInfo> GetRoleInUser(Int64 id)
        {
            var userRole = _dbContext.Set<UserInRole>().Where(it => it.RoleId == id);
            List<long> userIds = (from t in userRole select t.UserId).ToList();
            List<UserInfo> userList = new List<UserInfo>();
            if (userIds.Count > 0)
            {
                foreach (var v in userIds)
                {
                    UserInfo ui = GetUserInfo(v);
                    if (ui != null)
                    {
                        userList.Add(ui);
                    }
                }
            }
            return userList;
        }
        /// <summary>
        /// 根据角色名称获取使用该角色的用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserInfo> GetRoleInUser(string roleName)
        {
            List<RoleInfo> riList = _dbContext.Set<RoleInfo>().Where(it => it.RoleName.Contains(roleName)).ToList();
            List<UserInfo> userList = new List<UserInfo>();
            foreach (var ri in riList)
            {
                if (ri != null)
                {
                    var userRole = _dbContext.Set<UserInRole>().Where(it => it.RoleId == ri.Id);
                    List<long> userIds = (from t in userRole select t.UserId).ToList();
                    if (userIds.Count > 0)
                    {
                        foreach (var v in userIds)
                        {
                            UserInfo ui = GetUserInfo(v);
                            if (ui != null)
                            {
                                userList.Add(ui);
                            }
                        }
                    }
                }
            }
            return userList;
        }
        public bool DeleteUserInfo(long userId)
        {
            UserInfo ui = _dbContext.Set<UserInfo>().Where(it => it.Id == userId).Include(x => x.Roles).FirstOrDefault();
            _dbContext.Set<UserInfo>().Remove(ui);
            _dbContext.SaveChanges();
            return true;
        }


        public IQueryable<UserInfo> GetUserQueryable()
        {
            return _dbContext.Set<UserInfo>().AsQueryable();
        }


        public UserInfo CheckUserPassword(string userName, string password)
        {
            var userinfo = _dbContext.Set<UserInfo>().Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
            return userinfo;
        }

        public IQueryable<UserInfo> UserListIncludeRole(int startPage, int pageSize, out int rowCount, out int pageCount, Expression<Func<UserInfo, bool>> where = null, Expression<Func<UserInfo, object>> order = null, Expression<Func<UserInfo, DateTime>> orderTime = null)
        {
            var result = from p in _dbContext.Set<UserInfo>().Include(a=>a.Roles)
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
    }
}
