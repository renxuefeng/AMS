using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.Authorization;
using AMS.BLL.RoleApp;
using AMS.BLL.UserApp;
using AMS.Models;
using AMS.Models.Dto;
using AMS.Models.Entitys;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IUserInfoAppService _userInfoAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly ResponseData _responseData;
        public RoleController(IUserInfoAppService userInfoAppService,IRoleAppService roleAppService,ResponseData responseData, ILogger<RoleController> logger) :base(logger)
        {
            _userInfoAppService = userInfoAppService;
            _roleAppService = roleAppService;
            _responseData = responseData;
        }
        /// <summary>
        /// 查看权限列表
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.查看权限列表)]
        [HttpGet("GetModuleList")]
        public ActionResult<ResponseData> GetModuleList()
        {
            Dictionary<int, string> pList = new Dictionary<int, string>();
            foreach (ModulesType foo in Enum.GetValues(typeof(ModulesType)))
            {
                pList.Add((int)foo, foo.ToString());
            }
            _responseData.Success = true;
            _responseData.Data = pList.ToArray();
            return _responseData;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.获取角色列表)]
        [HttpGet]
        public ActionResult<ResponseData> List(int startPage = 100, int pageSize = 100)
        {
            //_responseData.Success = true;
            //_responseData.Data = _roleAppService.GetAllList();
            //return _responseData;
            int count = 0;
            int pagecount = 0;
            _responseData.Success = true;
            List<RoleInfo> riList = _roleAppService.GetAllPageList(startPage, pageSize, out count, out pagecount, null);
            riList.ForEach(x =>
            {
                x.Down["name"] = _userInfoAppService.GetUserInfo(x.CreateUserID)?.UserName;
                // 获取关联权限信息
                _roleAppService.GetRoleInfo(x.Id);
            });
            PageInfo pageInfo = new PageInfo();
            pageInfo.data = riList;
            pageInfo.rowCount = count;
            pageInfo.pageCount = pagecount;
            _responseData.Data = pageInfo;
            return _responseData;
        }
        /// <summary>
        /// 获取角色列表(分页)
        /// </summary>
        /// <returns></returns>
        //[WebMethodAction(ModulesType.获取角色列表)]
        //[HttpGet]
        //public ActionResult<ResponseData> ListByPage(int startPage, int pageSize)
        //{
        //    int count = 0;
        //    int pagecount = 0;
        //    _responseData.Success = true;
        //    _responseData.Data = _roleAppService.GetAllPageList(startPage, pageSize, out count, out pagecount,null);
        //    return _responseData;
        //}
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.新增角色信息)]
        [HttpPost]
        public ActionResult<ResponseData> Create(RoleInfoDto roleInfoDto)
        {
            if (_roleAppService.GetRoleInfo(roleInfoDto.RoleName) == null)
            {
                //List<long> menuIDS = roleInfoDto.MenuIDS?.Where(x => x > 0).ToList();
                List<long> moduleIDS = roleInfoDto.ModuleIDS?.Where(x => x > 0).ToList();
                roleInfoDto.CreateUserID = UserId;
                roleInfoDto.CreateDateTime = DateTime.Now;
                _responseData.Success = _roleAppService.InsertOrUpdate(roleInfoDto, null, moduleIDS);
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "角色名称已存在";
            }
            return _responseData;
        }
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.获取角色信息)]
        [HttpGet("{id}")]
        public ActionResult<ResponseData> Get(long id)
        {
            _responseData.Success = true;
            _responseData.Data = _roleAppService.GetRoleInfo(id);
            return _responseData;
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.删除角色信息)]
        [HttpDelete("{id}")]
        public ActionResult<ResponseData> Delete(long id)
        {
            _responseData.Success = _roleAppService.Delete(id);
            return _responseData;
        }
        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <returns></returns>
        [WebMethodAction(ModulesType.编辑角色信息)]
        [HttpPut]
        public ActionResult<ResponseData> Update(RoleInfoDto roleInfoDto)
        {
            if (roleInfoDto.Id > 0)
            {
                var info = _roleAppService.GetRoleInfo(roleInfoDto.Id);
                if (info != null)
                {
                    List<long> menuIDS = roleInfoDto.MenuIDS?.Where(x=>x>0).ToList();
                    List<long> moduleIDS = roleInfoDto.ModuleIDS?.Where(x => x > 0).ToList();
                    _responseData.Success = _roleAppService.InsertOrUpdate(info, menuIDS, moduleIDS);
                }
            }
            return _responseData;
        }
    }
}
