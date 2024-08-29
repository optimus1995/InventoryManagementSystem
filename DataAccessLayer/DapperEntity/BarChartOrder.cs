using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{
    public class BarChartOrder
    {
        public long Amount { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }  
        public int Day { get; set; }
    }

}
