using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Orders.GetGraphChart

{
    public class GraphChartRequest : IRequest<GraphChartResponse>
    {


        public int Amount { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }
    }
}
