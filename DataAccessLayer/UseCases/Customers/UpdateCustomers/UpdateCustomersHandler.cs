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
using AutoMapper;

namespace ApplicationCore.UseCases.Customers.UpdateCustomers
{
    public class UpdateCustomersHandler : IRequestHandler<UpdateCustomersRequest, UpdateCustomersResponse>

    {
        private readonly ICustomersRepository _CustomersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;



        public UpdateCustomersHandler(ICustomersRepository CustomersRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _CustomersRepository = CustomersRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<UpdateCustomersResponse> Handle(UpdateCustomersRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var customers = new DapperEntity.Customers
            //{
            //    Id = request.Id,
            //    Name = request.Name,
            //    ShippingAddress = request.ShippingAddress,
            //    BillingAddress = request.BillingAddress,
            //    Email= request.Email,
            //    CreatedAt = request.CreatedAt,

            //    CreatedBy = request.CreatedBy,
            //    UpdatedAt = DateTime.Now,
            //    UpdatedBy=userid
            //};
            var customers = _mapper.Map<DapperEntity.Customers>(request);
            var UpdatedCustomers = await _CustomersRepository.Update(customers);

            return new UpdateCustomersResponse();
           
        }
    }
}

