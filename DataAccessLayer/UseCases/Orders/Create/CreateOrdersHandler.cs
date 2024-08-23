using ApplicationCore.Contract;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Http;


namespace ApplicationCore.UseCases.Orders.Create
{
    public class CreateOrdersHandler : IRequestHandler<CreateOrdersRequest, CreateOrdersResponse>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrdersRepository _ordersRepository;

        public CreateOrdersHandler(IOrdersRepository ordersRepository,
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

        public async Task<CreateOrdersResponse> Handle(CreateOrdersRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //   var customer = await _customersRepository.GetrecordforUpdate(request.CustomerId);
            var products = (await _productsRepository.GetAll(userid)).ToList();
            var customers = (await _customersRepository.GetAll(userid)).ToList();
            var categories = (await _categoryRepository.GetAll()).ToList();

            var response = new CreateOrdersResponse
            {
                //CustomerId = customer.Id,
                //CustomerName = customer.Name,
                //CustomerBillingAddress = customer.BillingAddress,
                //CustomerShippingAddress = customer.ShippingAddress,
                Products = products,
                Customers = customers,
                Categories = categories
            };

            return response;
        }
    }
}
