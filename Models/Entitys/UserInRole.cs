using System;
using System.Collections.Generic;
using System.Text;

namespace AMS.Models.Entitys
{
    public class UserInRole : Entity
    {
        public long UserId { get; set; }
        public UserInfo UserInfo { get; set; }

        public long RoleId { get; set; }
        public RoleInfo RoleInfo { get; set; }
    }
}
