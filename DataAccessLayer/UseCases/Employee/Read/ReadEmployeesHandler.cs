using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApplicationCore.UseCases.Category.ReadCategory;

namespace ApplicationCore.UseCases.Employee.Read
{
    public class ReadCustomersHandler : IRequestHandler<ReadEmployeesRequest, ReadEmployeesResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReadCustomersHandler( IEmployeesRepository employeesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _employeesRepository = employeesRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ReadEmployeesResponse> Handle(ReadEmployeesRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var records = await _employeesRepository.GetAll();
            var response = new ReadEmployeesResponse
            {AspNetUsers=records
            };
         
            return response;
        }
    }


}
