using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AMS.Models.Entitys
{
    /**********************************************************************************************//**
 * @class   RoleInfo
 *
 * @brief   权限信息实体
 *
 * @author  rxf
 * @date    2017/12/25
 **************************************************************************************************/
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Table("RoleInfo")]
    public class RoleInfo : Entity
    {
        /**********************************************************************************************//**
         * @property    public string RoleName
         *
         * @brief   角色名称
         *
         * @return  The name of the role.
         **************************************************************************************************/
        [Required]
        //[Remote("CheckUserName", "Role", ErrorMessage = "角色名称不能重复")]
        //[Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        [JsonProperty("roleName")]
        [StringLength(20, ErrorMessage = "角色名称长度不能超过20个字符")]
        public string RoleName { get; set; }
        /**********************************************************************************************//**
         * @property    public string Description
         *
         * @brief   角色描述
         *
         * @return  The description.
         **************************************************************************************************/
        //[Column(TypeName = "nvarchar")]
        [MaxLength(100)]
        [JsonProperty("description")]
        [StringLength(20, ErrorMessage = "备注长度不能超过20个字符")]
        public string Description { get; set; }
        [Required]
        [JsonProperty("createDateTime")]
        public DateTime CreateDateTime { get; set; }
        [Required]
        [JsonProperty("createUserID")]
        public long CreateUserID { get; set; }

        public List<UserInRole> Users { get; set; }

        public List<RoleInModule> Modules { get; set; }
        public List<RoleInMenu> Menus { get; set; }
    }
}
