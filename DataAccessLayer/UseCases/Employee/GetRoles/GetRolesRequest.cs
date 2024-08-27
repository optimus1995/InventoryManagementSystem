using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Employee.GetRoles;
using MediatR;


namespace ApplicationCore.UseCases.Employee.GetRoles
{
    public class GetRolesRequest : IRequest<GetRolesResponse>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
