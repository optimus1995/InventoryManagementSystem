using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApplicationCore.UseCases.Category.ReadCategory;
using AutoMapper;

namespace ApplicationCore.UseCases.Customers.ReadCustomers
{
    public class ReadCustomersHandler : IRequestHandler<ReadCustomersRequest, ReadCustomersResponse>
    {
        private readonly ICustomersRepository _CustomersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ReadCustomersHandler(ICustomersRepository CustomersRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {

            _CustomersRepository = CustomersRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ReadCustomersResponse> Handle(ReadCustomersRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var records = await _CustomersRepository.GetAll(userid);
           var response = _mapper.Map<ReadCustomersResponse>(records);
            //var response = new ReadCustomersResponse
            //{
            //    Customers = records

            //};
            return response;
        }
    }

   
}
