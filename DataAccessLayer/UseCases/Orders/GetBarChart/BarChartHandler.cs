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
            var records = await _ordersRepository.BarChartOrderDetail();
            return new BarChartResponse
            {
            };
        }
    }

   
}
