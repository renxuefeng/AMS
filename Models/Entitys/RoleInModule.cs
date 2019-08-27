using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AMS.Models.Entitys
{
    public class RoleInModule : Entity
    { 
        public long RoleId { get; set; }
        public RoleInfo RoleInfo { get; set; }
        public long ModuleID { get; set; }
    }
}
