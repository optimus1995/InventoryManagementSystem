using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Category.Update;

namespace ApplicationCore.UseCases.Customers.Update
{
    public class FetchCustomersHandler: IRequestHandler<FetchCustomersRequest,  FetchCustomersResponse>
    //    {
    //    }
    //}
    { 
        private readonly ICustomersRepository _CustomersRepository;

        public FetchCustomersHandler(ICustomersRepository CustomersRepository)
        {
            _CustomersRepository = CustomersRepository;
        }


        public async Task<FetchCustomersResponse> Handle(FetchCustomersRequest request, CancellationToken cancellationToken)
        {

            var customers = await _CustomersRepository.GetrecordforUpdate(request.Id);
            var customerss = new FetchCustomersResponse
            {
                Name = customers.Name,
                Id = customers.Id,
                BillingAddress = customers.BillingAddress,
                ShippingAddress = customers.ShippingAddress,
                CreatedAt = customers.CreatedAt,
                CreatedBy = customers.CreatedBy,
                Email = customers.Email,






            };
            return customerss;

        }
    }
}

