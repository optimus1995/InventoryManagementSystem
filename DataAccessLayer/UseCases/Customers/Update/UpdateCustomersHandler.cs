using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.UseCases.Customers.Update
{
    public class UpdateCustomersHandler : IRequestHandler<UpdateCustomersRequest, UpdateCustomersResponse>

    {
        private readonly ICustomersRepository _CustomersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UpdateCustomersHandler(ICustomersRepository CustomersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _CustomersRepository = CustomersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateCustomersResponse> Handle(UpdateCustomersRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var customers = new DapperEntity.Customers
            {
                Id = request.Id,
                Name = request.Name,
                ShippingAddress = request.ShippingAddress,
                BillingAddress = request.BillingAddress,
                Email= request.Email,
                CreatedAt = request.CreatedAt,
               
                CreatedBy = request.CreatedBy,
                UpdatedAt = DateTime.Now,
                UpdatedBy=userid
                

            };
            var UpdatedCustomers = await _CustomersRepository.Update(customers);

            return new UpdateCustomersResponse
            {
                Id = UpdatedCustomers.Id,
                Name = UpdatedCustomers.Name,
                BillingAddress = UpdatedCustomers.BillingAddress,
                ShippingAddress = UpdatedCustomers.ShippingAddress,
                Email = UpdatedCustomers.Email,
            };
        }
    }
}

