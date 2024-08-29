using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Orders.GetBarChart
{
    public class BarChartHandler : IRequestHandler<BarChartRequest, BarChartResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public BarChartHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<BarChartResponse> Handle(BarChartRequest request, CancellationToken cancellationToken)
        {
            var record = await _ordersRepository.BarChartOrderDetail();

            var barChartOrders = record.Select(record => new BarChartOrder
            {
                // Map properties from BarChartResponse to BarChartOrder
                Amount = record.Amount,
                Year = record.Year,
                Month = record.Month,
                Day = record.Day,
                // Add other mappings as needed
            }).ToList();

            return new BarChartResponse
            {
                barChartOrders = barChartOrders
            };

        }
    }

   
}
