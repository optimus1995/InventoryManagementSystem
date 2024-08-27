using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employee.GetRoles
{
    public class GetRolesResponse
    {

        public IEnumerable<AspNetRoles> AspNetRoles { get; set; }   
        public string Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
