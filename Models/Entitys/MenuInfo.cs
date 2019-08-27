using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AMS.Models.Entitys
{
    /**********************************************************************************************//**
 * @class   MenuInfo
 *
 * @brief   功能信息表.
 *
 * @author  qyz
 * @date    2017/12/28
 **************************************************************************************************/
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Table("MenuInfo")]
    public class MenuInfo : Entity
    {
        /**********************************************************************************************//**
         * @property    public string MenuName
         *
         * @brief   功能名称
         *
         * @return  The name of the menu.
         **************************************************************************************************/
        [JsonProperty("menuName")]
        [Required(ErrorMessage = "请输入菜单名称")]
        [MaxLength(20, ErrorMessage = "菜单长度不能超过20个字符")]
        public string MenuName { get; set; }

        /**********************************************************************************************//**
         * @property    public int MenuType
         *
         * @brief   功能类型
         *
         * @return  The type of the menu.
         **************************************************************************************************/
        [JsonProperty("menuType")]
        [Range(1, 3, ErrorMessage = "菜单类型输入错误")]
        [Required(ErrorMessage = "请选择菜单类型")]
        public int MenuType { get; set; }

        /**********************************************************************************************//**
         * @property    public string Url
         *
         * @brief   链接地址
         *
         * @return  The URL.
         **************************************************************************************************/
        [JsonProperty("url")]
        //[Required(ErrorMessage ="请输入菜单URL")]
        [MaxLength(200, ErrorMessage = "菜单URL不能超过200字符")]
        public string Url { get; set; }

        /**********************************************************************************************//**
         * @property    public Int64 ParentID
         *
         * @brief   父级ID
         *
         * @return  The identifier of the parent.
         **************************************************************************************************/
        [JsonProperty("parentId")]
        //[Range(1, 9223372036854775807, ErrorMessage = "请输入合法的父菜单ID")]
        public Int64 ParentID { get; set; }

        /**********************************************************************************************//**
         * @property    public string MenuPic
         *
         * @brief   功能图标地址
         *
         * @return  The menu picture.
         **************************************************************************************************/
        [JsonProperty("menuPic")]
        [MaxLength(50, ErrorMessage = "菜单图标的长度不能超过50字符")]
        public string MenuPic { get; set; }

        /**********************************************************************************************//**
         * @property    public int Status
         *
         * @brief   状态
         *
         * @return  The status.
         **************************************************************************************************/
        [JsonProperty("status")]
        [Required]
        [Range(0, 1, ErrorMessage = "请输入合法的菜单状态")]
        public int Status { get; set; }



        /**********************************************************************************************//**
         * @property    public int SortIndex
         *
         * @brief   菜单排序值
         *
         * @return  The sort index.
         **************************************************************************************************/
        [JsonProperty("sortIndex")]
        [Range(1, 100, ErrorMessage = "请输入1~100的整数")]
        public int? SortIndex { get; set; }

        /**********************************************************************************************//**
         * @property    public string ModulesIDS
         *
         * @brief   权限信息（逗号隔开）
         *
         * @return  A list of identifiers of the modules.
         **************************************************************************************************/
        //[JsonProperty("modulesIDS")]
        //[MaxLength(1000)]
        //public string ModulesIDS { get; set; }

        /**********************************************************************************************//**
         * @property    public MenuInfo Children
         *
         * @brief   子机构
         *
         * @return  The children.
         **************************************************************************************************/
        [JsonProperty("children")]
        [NotMapped]
        public List<MenuInfo> Children { get; set; }

        public List<MenuInModule> Modules { get; set; }

        public List<RoleInMenu> Roles { get; set; }
    }
}
