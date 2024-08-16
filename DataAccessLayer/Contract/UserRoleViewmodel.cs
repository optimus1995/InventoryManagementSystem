using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract
{
    public class UserRoleViewModel
    {
        public AspNetUsers User { get; set; }
        public AspNetRoles Role { get; set; }
    }
}
