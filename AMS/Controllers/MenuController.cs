using AMS.Authorization;
using AMS.BLL.MenuApp;
using AMS.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.Controllers
{
    public class MenuController : BaseController
    {
        private readonly ResponseData _responseData;
        private readonly IMenuInfoAppService _menuInfoAppService;
        public MenuController(IMenuInfoAppService menuInfoAppService, ILogger<MenuController> logger, ResponseData responseData) : base(logger)
        {
            _menuInfoAppService = menuInfoAppService;
            _responseData = responseData;
        }
        /// <summary>
        /// 查看权限列表
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.查看菜单列表)]
        [HttpGet("GetMenuList")]
        public ActionResult<ResponseData> GetMenuList()
        {
            _responseData.Success = true;
            _responseData.Data = _menuInfoAppService.GetAllListIncludeModules();
            return _responseData;
        }
    }
}
