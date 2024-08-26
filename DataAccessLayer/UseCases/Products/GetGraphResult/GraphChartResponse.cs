using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{ 
    public class GraphChartResponse
    {

        public IEnumerable<ApplicationCore.DapperEntity.ProductCategoryGraph> productCategoryGraphs { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }

    }
}
