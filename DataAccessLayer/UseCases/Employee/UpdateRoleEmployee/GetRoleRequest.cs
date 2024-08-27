using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Employee.UpdateRoleEmployee
{
    public class GetRoleRequest : IRequest<GetRoleResponse>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public DateTimeOffset? LockOutEnd { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public AspNetRoles? AspNetRoles { get; set; }
        public AspNetUserRoles? AspNetUserRoles { get; set; }

    }
}
