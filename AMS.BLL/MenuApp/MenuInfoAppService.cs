using AMS.DAL.IRepositories;
using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.BLL.MenuApp
{
    public class MenuInfoAppService : IMenuInfoAppService
    {

        private readonly IMenuRepository _repository;
        public MenuInfoAppService(IMenuRepository repository)
        {
            _repository = repository;
        }
        public List<MenuInfo> GetAllListIncludeModules()
        {
            return _repository.GetAllListIncludeModules();
        }
    }
}
