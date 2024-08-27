using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Employee.UpdateRoleEmployee
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleRequest, UpdateRoleResponse>
    //    {
    //    }
    //}
    {
        private readonly IEmployeesRepository _employeesRepository;

        public UpdateRoleHandler(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;

        }
        public async Task<UpdateRoleResponse> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {


            var role =  _employeesRepository.UpdateRoles(request);
            var response = new UpdateRoleResponse();

            return (response);

            
           
        }
    }
}

