using ApplicationCore.Contract;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace ApplicationCore.UseCases.Customers.CreateCustomers
{
    public class SaveCustomersHandler : IRequestHandler<SaveCustomersRequest, SaveCustomersResponse>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public SaveCustomersHandler(
            ICustomersRepository customersRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _customersRepository = customersRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        //public async Task<SaveCustomersResponse> Handle(SaveCustomersRequest request, CancellationToken cancellationToken)
        //{

        //    var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);



        //    request.CreatedAt = DateTime.Now;
        //    request.CreatedBy = userid;
        //   var customers= _mapper.Map<ApplicationCore.DapperEntity.Customers>(request);
        //    //var customers = new ApplicationCore.DapperEntity.Customers
        //    //{
        //    //   Name = request.Name,
        //    //   BillingAddress = request.BillingAddress,
        //    //   ShippingAddress = request.ShippingAddress,   
        //    //   CreatedAt = DateTime.Now,    
        //    //   CreatedBy = userid,
        //    //   Emai l = request.Email


        //    //};


        //    var record = await _customersRepository.CreateRecord(customers);
        //    var customerss = new SaveCustomersResponse();
        //           return customerss;

        //}

        public async Task<SaveCustomersResponse> Handle(SaveCustomersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                request.CreatedAt = DateTime.Now;
                request.CreatedBy = userId;

                var customers = _mapper.Map<ApplicationCore.DapperEntity.Customers>(request);

                var record = await _customersRepository.CreateRecord(customers);

                var customersResponse = new SaveCustomersResponse();
                return customersResponse;
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework, for example)
                throw;
            }
        }

    }
}
