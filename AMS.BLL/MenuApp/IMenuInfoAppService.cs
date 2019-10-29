using AMS.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.BLL.MenuApp
{
    public interface IMenuInfoAppService
    {
        List<MenuInfo> GetAllListIncludeModules();
    }
}
