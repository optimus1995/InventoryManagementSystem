using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Orders.SpecificOrder
{
    public class ReadOrdersHandler : IRequestHandler<SpecificOrdersRequest, SpecificOrdersResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public ReadOrdersHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<SpecificOrdersResponse> Handle(SpecificOrdersRequest request, CancellationToken cancellationToken)
        {
            int id= request.Id;
            var records = await _ordersRepository.ResultByOrderId(id);
            return new SpecificOrdersResponse
            {
              Orders = records,
            };
        }
    }

   
}
