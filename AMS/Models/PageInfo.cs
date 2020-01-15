using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Models
{
    public class PageInfo
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [Range(1, 2147483647, ErrorMessage = "请输入正确的当前页码")]
        public int startPage { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        [Range(1, 2147483647, ErrorMessage = "请输入正确的当前页面显示数量")]
        public int pageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int rowCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; }
        public object data { get; set; }
    }
}
