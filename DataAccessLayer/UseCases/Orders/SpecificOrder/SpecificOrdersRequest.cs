using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Orders.SpecificOrder
{
    public class SpecificOrdersRequest : IRequest<SpecificOrdersResponse>
    {

        public int Id { get; set; }
       
    }
}
