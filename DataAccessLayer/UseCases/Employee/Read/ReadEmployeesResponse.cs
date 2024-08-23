using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Employee.Read
{
    public class ReadEmployeesResponse
    {


        public IEnumerable<ApplicationCore.DapperEntity.AspNetUsers> AspNetUsers { get; set; }

    }
}
