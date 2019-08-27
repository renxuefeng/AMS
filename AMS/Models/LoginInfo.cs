using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class LoginInfo
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage ="登录名不能为空")]
        public string userName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="登录密码不能为空")]
        public string password { get; set; }
    }
}
