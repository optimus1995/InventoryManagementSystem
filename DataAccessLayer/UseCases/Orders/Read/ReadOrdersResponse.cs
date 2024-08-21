using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{ 
    public class ReadOrdersResponse
    {
        public IEnumerable<OrderItems> Orders { get; set; }
    }
}
