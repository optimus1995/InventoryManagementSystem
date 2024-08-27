using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Category.UpdateCategory;
using AutoMapper;

namespace ApplicationCore.UseCases.Customers.UpdateCustomers
{
    public class FetchCustomersHandler: IRequestHandler<FetchCustomersRequest,  FetchCustomersResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICustomersRepository _CustomersRepository;
        private readonly IMapper _mapper;

        public FetchCustomersHandler(ICustomersRepository CustomersRepository,
            IMapper mapper)
        {
            _CustomersRepository = CustomersRepository;
            _mapper = mapper;   
        }


        public async Task<FetchCustomersResponse> Handle(FetchCustomersRequest request, CancellationToken cancellationToken)
        {

            var customers = await _CustomersRepository.GetrecordforUpdate(request.Id);
            //var customerss = new FetchCustomersResponse
            //{
            //    Name = customers.Name,
            //    Id = customers.Id,
            //    BillingAddress = customers.BillingAddress,
            //    ShippingAddress = customers.ShippingAddress,
            //    CreatedAt = customers.CreatedAt,
            //    CreatedBy = customers.CreatedBy,
            //    Email = customers.Email,






            //};

            var customerss = _mapper.Map<FetchCustomersResponse>(customers);
            return customerss;

        }
    }
}

