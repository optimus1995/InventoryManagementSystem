using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Linq;

namespace ApplicationCore.UseCases.Orders.GetGraphChart
{
    public class GraphChartHandler : IRequestHandler<GraphChartRequest, GraphChartResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GraphChartHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<GraphChartResponse> Handle(GraphChartRequest request, CancellationToken cancellationToken)
        {
            var record = await _ordersRepository.BarChartOrderDetail();
            var rec = record.ToList();

            var records = record.FirstOrDefault(r =>
                 r.Year == request.Year &&
                 r.Month == request.Month && 
                 r.Amount==request.Amount);

            // Create and return the GraphChartResponse
            return new GraphChartResponse
            {
                Amount =request.Amount,  // Use null-coalescing operator to handle null
                Year = request.Year,
                Month = request.Month
            };
        }
    }


}
