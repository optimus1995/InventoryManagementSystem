using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Employee.UpdateRole
{
    public class GetRoleHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
    //    {
    //    }
    //}
    {
        private readonly IEmployeesRepository _employeesRepository;

        public GetRoleHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;

        }
        public async Task<GetRoleResponse> Handle(GetRoleRequest request, CancellationToken cancellationToken)
        {


            var role = await _employeesRepository.GetRoleData(request.Id);

            var viewModel = new GetRoleResponse
            {
                UserId = role.Id,
                UserName = role.UserName,
                RoleId = role.AspNetUserRoles.RoleId,
                RoleName = role.AspNetRoles.NormalizedName,
            };

            return viewModel;
           
        }
    }
}

