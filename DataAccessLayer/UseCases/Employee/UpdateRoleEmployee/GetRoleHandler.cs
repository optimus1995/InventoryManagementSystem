using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using AutoMapper;

namespace ApplicationCore.UseCases.Employee.UpdateRoleEmployee
{
    public class GetRoleHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
    //    {
    //    }
    //}
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;

        public GetRoleHandler(IEmployeesRepository employeesRepository,
            IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;   

        }
        public async Task<GetRoleResponse> Handle(GetRoleRequest request, CancellationToken cancellationToken)
        {
            

            var role = await _employeesRepository.GetRoleData(request.Id);
            var viewModel = _mapper.Map<GetRoleResponse>(role);
            //var viewModel = new GetRoleResponse
            //{
            //    UserId = role.Id,
            //    UserName = role.UserName,
            //    RoleId = role.AspNetUserRoles.RoleId,
            //    RoleName = role.AspNetRoles.NormalizedName,
            //};

            return viewModel;
           
        }
    }
}

