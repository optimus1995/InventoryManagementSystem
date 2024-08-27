using ApplicationCore.Contract;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.UseCases.Orders.CreateOrders
{
    public class SaveOrdersHandler : IRequestHandler<SaveOrdersRequest, SaveOrdersResponse>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrdersRepository _ordersRepository;

        public SaveOrdersHandler(IOrdersRepository ordersRepository,
            ICustomersRepository customersRepository,
            IProductsRepository productsRepository,
            ICategoryRepository categoryRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SaveOrdersResponse> Handle(SaveOrdersRequest request, CancellationToken cancellationToken)
        {
           
                var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                
                request.OrderStatus = "Processing";
                request.IsActive = true;
                request.CreatedAt = DateTime.Now;
                request.CreatedBy = userid;

          var record=  await   _ordersRepository.CreateOrders(request);

            return record;  
           
        }
    }
}
