using ApplicationCore.Contract;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Http;
using AutoMapper;


namespace ApplicationCore.UseCases.Orders.CreateOrders
{
    public class CreateOrdersHandler : IRequestHandler<CreateOrdersRequest, CreateOrdersResponse>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper ;

        public CreateOrdersHandler(IOrdersRepository ordersRepository,
            ICustomersRepository customersRepository,
            IProductsRepository productsRepository,
            ICategoryRepository categoryRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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

                Products = products,
                Customers = customers,
                Categories = categories
            };
            //    var response = _mapper.Map<CreateOrdersResponse>((products,customers,categories,));

            return response;
        }
    }
}
