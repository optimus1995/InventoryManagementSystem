using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employee.UpdateRoleEmployee
{ 
    public class UpdateRoleResponse
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string UserId { get; set; }
    }
}
