using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{ 
    public class BarChartResponse
    {


        public IEnumerable<ApplicationCore.DapperEntity.BarChartOrder> barChartOrders { get; set; }
        public int Amount { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }
    }
}
