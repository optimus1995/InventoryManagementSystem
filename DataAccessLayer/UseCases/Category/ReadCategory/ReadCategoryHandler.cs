using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using AutoMapper;

namespace ApplicationCore.UseCases.Category.ReadCategory
{
    public class ReadCategoryHandler : IRequestHandler<ReadCategoryRequest, ReadCategoryResponse>
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;


        public ReadCategoryHandler(ICategoryRepository CategoryRepository,
            IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }

        public async Task<ReadCategoryResponse> Handle(ReadCategoryRequest request, CancellationToken cancellationToken)
        {
            var records = await _CategoryRepository.GetAll();
           var response = _mapper.Map<ReadCategoryResponse>(records);
            //var response =  new ReadCategoryResponse
            //{
            //   Categories=records

            //};
            return response;
        }
    }

   
}
