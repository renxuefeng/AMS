using AMS.Models.Entitys;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            modelBuilder.Entity<UserInfo>();

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

            modelBuilder.Entity<RoleInMenu>()
                .HasKey(pt => new { pt.RoleId, pt.MenuId });
            modelBuilder.Entity<RoleInMenu>()
                .HasOne(pt => pt.RoleInfo)
                .WithMany(p => p.Menus)
                .HasForeignKey(pt => pt.RoleId);
            modelBuilder.Entity<RoleInMenu>()
                .HasOne(pt => pt.MenuInfo)
                .WithMany(t => t.Roles)
                .HasForeignKey(pt => pt.MenuId);

            modelBuilder.Entity<MenuInModule>()
                .HasKey(pt => new { pt.MenuID });
            modelBuilder.Entity<MenuInModule>()
                .HasOne(pt => pt.MenuInfo)
                .WithMany(p => p.Modules)
                .HasForeignKey(pt => pt.MenuID);

            modelBuilder.Entity<RoleInModule>()
                .HasKey(pt => new { pt.Id });
            modelBuilder.Entity<RoleInModule>()
                .HasOne(pt => pt.RoleInfo)
                .WithMany(p => p.Modules)
                .HasForeignKey(pt => pt.RoleId);

            //modelBuilder.Entity<LogInfo>()
            //    .HasKey(pt => new { pt.Id });

            SeedData(modelBuilder);


        }
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            UserInfo ui = new UserInfo
            {
                Id = 1511,
                UserName = "admin",
                Password = "123456",
                UserType = (int)UserTypeEnum.超级管理员,
                Gender = (int)GenderType.男,
                Status = (int)UserStatus.启用,
                CreateUserID = 0000,
                CreateUserTime = DateTime.Now
            };
            modelBuilder.Entity<UserInfo>().HasData(ui);
        }
    }
}
