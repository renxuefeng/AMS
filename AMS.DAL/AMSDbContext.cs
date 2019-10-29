using AMS.Models.Entitys;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DAL
{
    public class AMSDbContext : DbContext
    {
        public AMSDbContext(DbContextOptions<AMSDbContext> options) : base(options)
        {

        }
        public DbSet<UserInfo> UserInfo { get; set; }
        //public DbSet<UserInRole> UserInRole { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<UserInRole> UserInRole { get; set; }
        public DbSet<RoleInMenu> RoleInMenu { get; set; }
        public DbSet<RoleInModule> RoleInModule { get; set; }
        public DbSet<MenuInfo> MenuInfo { get; set; }
        //public DbSet<LogInfo> MogInfo { get; set; }
        //public DbSet<OperationLog> OperationLog { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
            .HasIndex(b => b.UserName)
            .IsUnique();
            // 时间默认值
            modelBuilder.Entity<UserInfo>()
                .Property(b => b.CreateUserTime)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<RoleInfo>()
                .Property(b => b.CreateDateTime)
                .HasDefaultValueSql("getdate()");

            // 用户角色关联表
            modelBuilder.Entity<UserInRole>()
            .HasKey(pt => new { pt.UserId, pt.RoleId });
            modelBuilder.Entity<UserInRole>()
                .HasOne(pt => pt.UserInfo)
                .WithMany(p => p.Roles)
                .HasForeignKey(pt => pt.UserId);
            modelBuilder.Entity<UserInRole>()
                .HasOne(pt => pt.RoleInfo)
                .WithMany(t => t.Users)
                .HasForeignKey(pt => pt.RoleId);
            
            // 角色菜单关联表
            modelBuilder.Entity<RoleInMenu>()
                .HasKey(pt => new { pt.RoleId, pt.MenuId });
            modelBuilder.Entity<RoleInMenu>()
                .HasOne(pt => pt.RoleInfo)
                .WithMany(p => p.Menus)
                .HasForeignKey(pt => pt.MenuId);
            modelBuilder.Entity<RoleInMenu>()
                .HasOne(pt => pt.MenuInfo)
                .WithMany(t => t.Roles)
                .HasForeignKey(pt => pt.RoleId);

            // 菜单权限关联表
            modelBuilder.Entity<MenuInModule>()
                .HasKey(pt => new { pt.MenuID, pt.ModuleID });
            modelBuilder.Entity<MenuInModule>()
                .HasOne(pt => pt.MenuInfo)
                .WithMany(p => p.Modules)
                .HasForeignKey(pt => pt.MenuID);

            // 菜单父目录ID默认0
            modelBuilder.Entity<MenuInfo>()
                .HasKey(c => c.Id);
            // 单一导航
            modelBuilder.Entity<MenuInfo>()
                .HasMany(b => b.Children)
                .WithOne()
                .HasForeignKey(e => e.ParentID)
                .OnDelete(DeleteBehavior.Restrict);
            SeedData(modelBuilder);
        }
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                Id = 1511,
                UserName = "admin",
                Password = "123456",
                UserType = (int)UserTypeEnum.超级管理员,
                Gender = (int)GenderType.男,
                Status = (int)UserStatus.启用,
                CreateUserID = 0000,
                CreateUserTime = DateTime.Now
            });
            // 菜单信息
            modelBuilder.Entity<MenuInfo>().HasData(new MenuInfo
            {
                Id = 1,
                MenuName = "一级菜单1",
                Status = 1,
            });
            modelBuilder.Entity<MenuInfo>().HasData(new MenuInfo
            {
                Id = 2,
                MenuName = "一级菜单2",
                Status = 1
            });
            modelBuilder.Entity<MenuInfo>().HasData(new MenuInfo
            {
                Id = 3,
                ParentID = 1,
                MenuName = "一级菜单3",
                Status = 1
            });
            // 菜单权限关联信息
            modelBuilder.Entity<MenuInModule>().HasData(new MenuInModule()
            {
                Id = 1,
                MenuID = 1,
                ModuleID = 2
            });
            modelBuilder.Entity<MenuInModule>().HasData(new MenuInModule()
            {
                Id = 2,
                MenuID = 1,
                ModuleID = 3
            });
            modelBuilder.Entity<MenuInModule>().HasData(new MenuInModule()
            {
                Id = 3,
                MenuID = 1,
                ModuleID = 4
            });
            // 角色信息
            modelBuilder.Entity<RoleInfo>().HasData(new RoleInfo
            {
                Id = 1,
                RoleName = "前台用户角色",
                CreateUserID = 0,
            });
            // 角色菜单关联信息
            modelBuilder.Entity<RoleInMenu>().HasData(
                new RoleInMenu()
                {
                    Id = 1,
                    MenuId = 1,
                    RoleId = 1,
                }
            );
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                Id = 1512,
                UserName = "rxf",
                Password = "123456",
                UserType = (int)UserTypeEnum.前台用户,
                Gender = (int)GenderType.男,
                Status = (int)UserStatus.启用,
                CreateUserID = 0000,
                CreateUserTime = DateTime.Now,

            });
            // 用户角色关联信息
            modelBuilder.Entity<UserInRole>().HasData(
                new UserInRole()
                {
                    Id = 1,
                    UserId = 1512,
                    RoleId = 1
                }
            );
        }
    }
}
