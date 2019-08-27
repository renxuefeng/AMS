using AMS.Models.Entitys;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMS.Models.Dto
{
    public class RoleInfoDto : RoleInfo
    {
        /// <summary>
        /// 菜单IDS集合
        /// </summary>
        [DefaultValue(null)]
        [JsonProperty("menuIDS")]
        public List<long> MenuIDS { get; set; }
        /// <summary>
        /// 模块IDS集合
        /// </summary>
        [JsonProperty("moduleIDS")]
        public List<long> ModuleIDS { get; set; }
    }
}
