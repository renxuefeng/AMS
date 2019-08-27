using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.Models.Entitys
{
    public class RoleInMenu : Entity
    {
        public long RoleId { get; set; }
        public RoleInfo RoleInfo { get; set; }

        public long MenuId { get; set; }
        public MenuInfo MenuInfo { get; set; }
    }
}
