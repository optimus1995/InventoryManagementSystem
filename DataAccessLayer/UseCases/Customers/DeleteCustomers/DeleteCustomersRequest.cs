using ApplicationCore.DapperEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Customers.DeleteCustomers
{
    public class DeleteCustomersRequest:IRequest<DeleteCustomersResponse>
    {

        public int Id { get; set; }
      
    }
}
