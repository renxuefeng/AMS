using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS.Models.Entitys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [Table("UserInfo")]
    public class UserInfo : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty("userName")]
        [Required]
        //[Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        [StringLength(14, ErrorMessage = "用户名长度不能超过14个字符")]
        public string UserName { get; set; }
        [JsonProperty("passWord")]
        //[Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        [StringLength(32, ErrorMessage = "密码长度不正确")]
        public string Password { get; set; }

        /**********************************************************************************************//**
         * @property    public string Name
         *
         * @brief   姓名
         *
         * @return  The name.
         **************************************************************************************************/

        [JsonProperty("name")]
        //[Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        [StringLength(20, ErrorMessage = "姓名长度不能超过20个字符")]
        public string Name { get; set; }

        /**********************************************************************************************//**
         * @property    public int Gender
         *
         * @brief   性别
         *
         * @return  The gender.
         **************************************************************************************************/

        [JsonProperty("gender")]
        [Required]
        public int Gender { get; set; }

        /**********************************************************************************************//**
         * @property    public int Status
         *
         * @brief   账户状态 启用or禁用
         *
         * @return  The status.
         **************************************************************************************************/

        [JsonProperty("status")]
        [Required]
        public int Status { get; set; }

        /**********************************************************************************************//**
         * @property    public string UserPic
         *
         * @brief   用户头像
         *
         * @return  The user picture.
         **************************************************************************************************/

        [JsonProperty("userPic")]
        //[Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string UserPic { get; set; }

        [JsonProperty("userType")]
        [Required]
        public int UserType { get; set; }

        /**********************************************************************************************//**
         * @property    public string WorkUnit
         *
         * @brief   工作单位
         *
         * @return  The work unit.
         **************************************************************************************************/

        [JsonProperty("workUnit")]
        //[Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        [StringLength(30, ErrorMessage = "工作单位长度不能超过30个字符")]
        public string WorkUnit { get; set; }

        /**********************************************************************************************//**
         * @property    public long CreateUserID
         *
         * @brief   创建人ID
         *
         * @return  The identifier of the create user.
         **************************************************************************************************/

        [Required]
        public long CreateUserID { get; set; }

        /**********************************************************************************************//**
         * @property    public DateTime CreateUserTime
         *
         * @brief   创建时间
         *
         * @return  The create user time.
         **************************************************************************************************/
        [JsonProperty("createUserTime")]
        public DateTime CreateUserTime { get; set; }

        [JsonProperty("roleIDS")]

        public List<UserInRole> Roles { get; set; }
    }
}
