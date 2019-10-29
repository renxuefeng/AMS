﻿// <auto-generated />
using System;
using AMS.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMS.DAL.Data.Migrations
{
    [DbContext(typeof(AMSDbContext))]
    partial class AMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AMS.Models.Entitys.MenuInModule", b =>
                {
                    b.Property<long>("MenuID");

                    b.Property<long>("ModuleID");

                    b.Property<long>("Id");

                    b.HasKey("MenuID", "ModuleID");

                    b.ToTable("MenuInModule");

                    b.HasData(
                        new
                        {
                            MenuID = 1L,
                            ModuleID = 2L,
                            Id = 1L
                        },
                        new
                        {
                            MenuID = 1L,
                            ModuleID = 3L,
                            Id = 2L
                        },
                        new
                        {
                            MenuID = 1L,
                            ModuleID = 4L,
                            Id = 3L
                        });
                });

            modelBuilder.Entity("AMS.Models.Entitys.MenuInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MenuName");

                    b.Property<long?>("ParentID");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ParentID");

                    b.ToTable("MenuInfo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            MenuName = "一级菜单1",
                            Status = 1
                        },
                        new
                        {
                            Id = 2L,
                            MenuName = "一级菜单2",
                            Status = 1
                        },
                        new
                        {
                            Id = 3L,
                            MenuName = "一级菜单3",
                            ParentID = 1L,
                            Status = 1
                        });
                });

            modelBuilder.Entity("AMS.Models.Entitys.RoleInMenu", b =>
                {
                    b.Property<long>("RoleId");

                    b.Property<long>("MenuId");

                    b.Property<long>("Id");

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("RoleInMenu");

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            MenuId = 1L,
                            Id = 1L
                        });
                });

            modelBuilder.Entity("AMS.Models.Entitys.RoleInModule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ModuleID");

                    b.Property<long>("RoleId");

                    b.Property<long?>("RoleInfoId");

                    b.HasKey("Id");

                    b.HasIndex("RoleInfoId");

                    b.ToTable("RoleInModule");
                });

            modelBuilder.Entity("AMS.Models.Entitys.RoleInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("CreateUserID");

                    b.Property<string>("Description")
                        .HasMaxLength(20);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("RoleInfo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserID = 0L,
                            RoleName = "前台用户角色"
                        });
                });

            modelBuilder.Entity("AMS.Models.Entitys.UserInRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.Property<long>("Id");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInRole");

                    b.HasData(
                        new
                        {
                            UserId = 1512L,
                            RoleId = 1L,
                            Id = 1L
                        });
                });

            modelBuilder.Entity("AMS.Models.Entitys.UserInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreateUserID");

                    b.Property<DateTime>("CreateUserTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Gender");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(32);

                    b.Property<int>("Status");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(14);

                    b.Property<string>("UserPic")
                        .HasMaxLength(100);

                    b.Property<int>("UserType");

                    b.Property<string>("WorkUnit")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("UserInfo");

                    b.HasData(
                        new
                        {
                            Id = 1511L,
                            CreateUserID = 0L,
                            CreateUserTime = new DateTime(2019, 10, 29, 15, 27, 33, 910, DateTimeKind.Local).AddTicks(7688),
                            Gender = 0,
                            Password = "123456",
                            Status = 0,
                            UserName = "admin",
                            UserType = 1
                        },
                        new
                        {
                            Id = 1512L,
                            CreateUserID = 0L,
                            CreateUserTime = new DateTime(2019, 10, 29, 15, 27, 33, 913, DateTimeKind.Local).AddTicks(1200),
                            Gender = 0,
                            Password = "123456",
                            Status = 0,
                            UserName = "rxf",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("AMS.Models.Entitys.MenuInModule", b =>
                {
                    b.HasOne("AMS.Models.Entitys.MenuInfo", "MenuInfo")
                        .WithMany("Modules")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AMS.Models.Entitys.MenuInfo", b =>
                {
                    b.HasOne("AMS.Models.Entitys.MenuInfo")
                        .WithMany("Children")
                        .HasForeignKey("ParentID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AMS.Models.Entitys.RoleInMenu", b =>
                {
                    b.HasOne("AMS.Models.Entitys.RoleInfo", "RoleInfo")
                        .WithMany("Menus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AMS.Models.Entitys.MenuInfo", "MenuInfo")
                        .WithMany("Roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AMS.Models.Entitys.RoleInModule", b =>
                {
                    b.HasOne("AMS.Models.Entitys.RoleInfo", "RoleInfo")
                        .WithMany()
                        .HasForeignKey("RoleInfoId");
                });

            modelBuilder.Entity("AMS.Models.Entitys.UserInRole", b =>
                {
                    b.HasOne("AMS.Models.Entitys.RoleInfo", "RoleInfo")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AMS.Models.Entitys.UserInfo", "UserInfo")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
