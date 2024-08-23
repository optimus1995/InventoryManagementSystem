using ApplicationCore.Contract;
using MediatR;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.UseCases.Customers.Create
{
    public class SaveCustomersHandler : IRequestHandler<SaveCustomersRequest, SaveCustomersResponse>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
   
        public SaveCustomersHandler(IOrdersRepository ordersRepository,
            ICustomersRepository customersRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _customersRepository = customersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SaveCustomersResponse> Handle(SaveCustomersRequest request, CancellationToken cancellationToken)
        {

            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


      
            request.CreatedAt = DateTime.Now;
            request.CreatedBy = userid;

            var customers = new ApplicationCore.DapperEntity.Customers
            {
               Name = request.Name,
               BillingAddress = request.BillingAddress,
               ShippingAddress = request.ShippingAddress,   
               CreatedAt = DateTime.Now,    
               CreatedBy = userid,
               Email = request.Email
               
               
            };


            var record = await _customersRepository.CreateRecord(customers);
            var customerss = new SaveCustomersResponse();
                   return customerss;

        }
    }
}
