using AMS.DAL.IRepositories;
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
        /// 根据RelationID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public UserInfo GetUserInfo(Int64 id)
        {
            var user = _dbContext.Set<UserInfo>().Include(x=>x.Roles).ThenInclude(x=>x.RoleInfo).ThenInclude(x=>x.Modules).FirstOrDefault(it => it.Id == id);
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
    }
}
