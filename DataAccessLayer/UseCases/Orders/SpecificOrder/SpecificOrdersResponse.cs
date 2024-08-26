using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Orders.SpecificOrder
{ 
    public class SpecificOrdersResponse
    {
        public IEnumerable<OrderItems> Orders { get; set; }
    }
}
