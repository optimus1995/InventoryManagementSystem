using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Employee.UpdateRoleEmployee;
using FluentValidation;
using MediatR;

namespace ApplicationCore.UseCases.Employee.UpdateRoleEmployee
{
    public class UpdateRoleRequest : IRequest<UpdateRoleResponse>
    {

        public String UserId { get; set; }
        public String RoleId { get; set; }


    }
}
public class UpdateroleRequestValidator : AbstractValidator <UpdateRoleRequest>
{
    public UpdateroleRequestValidator()
    {
        RuleFor(x=>x.RoleId).NotEmpty();

    }
}
