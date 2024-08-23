//using ApplicationCore.Contract;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ApplicationCore.DapperEntity;

//namespace ApplicationCore.UseCases.Customers.Delete
//{
//    public class DeleteCustomersHandler: IRequestHandler<DeleteCustomersRequest,  DeleteCustomersResponse>
//    //    {
//    //    }
//    //}
//    { 
//        private readonly ICustomersRepository _CustomersRepository;

//        public DeleteCustomersHandler(ICustomersRepository CustomersRepository)
//        {
//            _CustomersRepository = CustomersRepository;
//        }

//        public async Task<DeleteCustomersResponse> Handle(DeleteCustomersRequest request, CancellationToken cancellationToken)
//        {
//            await _CustomersRepository.DeleteRecord(request.Id);

//            return new DeleteCustomersResponse
//            {
//                Id = request.Id
//            };
//        }
//    }
//}

