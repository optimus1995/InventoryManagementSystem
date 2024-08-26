using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;

namespace ApplicationCore.UseCases.Category.ReadCategory
{
    public class ReadCategoryHandler : IRequestHandler<ReadCategoryRequest, ReadCategoryResponse>
    {
        private readonly ICategoryRepository _CategoryRepository;

        public ReadCategoryHandler(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<ReadCategoryResponse> Handle(ReadCategoryRequest request, CancellationToken cancellationToken)
        {
            var records = await _CategoryRepository.GetAll();
            var response =  new ReadCategoryResponse
            {
               Categories=records

            };
            return response;
        }
    }

   
}
