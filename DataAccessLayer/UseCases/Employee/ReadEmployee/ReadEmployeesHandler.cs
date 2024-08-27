using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApplicationCore.UseCases.Category.ReadCategory;
using ApplicationCore.UseCases.Employee.ReadEmployee;

using AutoMapper;

namespace ApplicationCore.UseCases.Employee.ReadEmployee
{
    public class ReadEmployeesHandler : IRequestHandler<ReadEmployeesRequest, ReadEmployeesResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        

        public ReadEmployeesHandler( IEmployeesRepository employeesRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ReadEmployeesResponse> Handle(ReadEmployeesRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var records = await _employeesRepository.GetAll();
            var response = _mapper.Map<ReadEmployeesResponse>(records);

            //var response = new ReadEmployeesResponse
            //{AspNetUsers=records
            //};
         
            return response;
        }
    }


}
