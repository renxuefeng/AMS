using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AMS.Models.Entitys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Table("MenuInfo")]
    public class MenuInfo : Entity
    {
        [JsonProperty("menuName")]
        public string MenuName { get; set; }

        /**********************************************************************************************//**
         * @property    public Int64 ParentID
         *
         * @brief   父级ID
         *
         * @return  The identifier of the parent.
         **************************************************************************************************/
        [JsonProperty("parentId")]
        public Int64? ParentID { get; set; }


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
        //[JsonProperty("sortIndex")]
        //[Range(1, 100, ErrorMessage = "请输入1~100的整数")]
        //public int? SortIndex { get; set; }

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
