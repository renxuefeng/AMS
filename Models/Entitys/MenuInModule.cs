using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.Models.Entitys
{
    public class MenuInModule : Entity
    {
        public long MenuID { get; set; }
        public MenuInfo MenuInfo { get; set; }

        public long ModuleID { get; set; }
    }
}
