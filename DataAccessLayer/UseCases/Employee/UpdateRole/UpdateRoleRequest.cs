using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Employee.UpdateRole
{
    public class UpdateRoleRequest : IRequest<UpdateRoleResponse>
    {

        public String UserId { get; set; }
        public String RoleId { get; set; }


    }
}
