using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Orders.ReadOrders
{
    public class ReadOrdersHandler : IRequestHandler<ReadOrdersRequest, ReadOrdersResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public ReadOrdersHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<ReadOrdersResponse> Handle(ReadOrdersRequest request, CancellationToken cancellationToken)
        {
            var records = await _ordersRepository.Result();
            return new ReadOrdersResponse
            {
                Orders = records
            };
        }
    }

   
}
