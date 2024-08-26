using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Products.GetGraphChart

{
    public class GraphChartRequest : IRequest<GraphChartResponse>
    {

        public string CategoryName { get; set; }
        public int ProductCount { get; set; }

    }
}
