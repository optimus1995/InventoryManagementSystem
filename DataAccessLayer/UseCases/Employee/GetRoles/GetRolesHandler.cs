using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApplicationCore.UseCases.Employee.GetRoles;
using AutoMapper;

namespace ApplicationCore.UseCases.Employee.GetRoles
{
    public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResponse>
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        

        public GetRolesHandler( IEmployeesRepository employeesRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        {
            var records = await _employeesRepository.GetAllRoles();

            var response =  _mapper.Map<GetRolesResponse>(records);

            return response;
        }
    }


}
