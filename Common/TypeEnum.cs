using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    class TypeEnum
    {
    }
    public enum ModulesType
    {
        新建用户信息 = 1,
        编辑用户信息,
        编辑用户基础信息,
        删除用户信息,
        修改密码,
        修改我的密码,
        获取用户列表,
        获取用户信息,
        获取当前用户信息,
        查看权限列表,
        获取角色列表,
        获取角色信息,
        新增角色信息,
        编辑角色信息,
        删除角色信息,
        查看菜单列表
    }
    public enum DatabaseType
    {
        SQLServer
    }
    /// <summary>
    /// 用户类型枚举
    /// </summary>
    public enum UserTypeEnum
    {
        前台用户,
        超级管理员,
        后台用户,
        数据管理,
    }
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum GenderType
    {
        男 = 0,
        女
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        启用,
        禁用
    }
}
