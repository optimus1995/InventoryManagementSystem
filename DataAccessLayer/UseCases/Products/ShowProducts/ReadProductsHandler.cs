using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ApplicationCore.UseCases.Category.ReadCategory;

namespace ApplicationCore.UseCases.Products.Read
{
    public class ReadProductsHandler : IRequestHandler<ReadProductsRequest, ReadProductsResponse>
    {
        private readonly IProductsRepository _ProductsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryRepository _categoryRepository;

        public ReadProductsHandler(IProductsRepository ProductsRepository, IHttpContextAccessor httpContextAccessor, ICategoryRepository categoryRepository)
        {

            _ProductsRepository = ProductsRepository;
            _httpContextAccessor = httpContextAccessor;
            _categoryRepository = categoryRepository;

        }

        public async Task<ReadProductsResponse> Handle(ReadProductsRequest request, CancellationToken cancellationToken)
        {
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var records = await _ProductsRepository.GetAll(userid);
            //var response = new ReadProductsResponse
            //{
            //    Products = records

            //};
            //return response;

            var catid = request.catid;
            var categories = (await _categoryRepository.GetAll()).ToList();
            List<ApplicationCore.DapperEntity.Products> records;

            if (catid == null || catid == 0)
            {
                records = (List<ApplicationCore.DapperEntity.Products>)await _ProductsRepository.GetAll(userid);
            }
            else
            {
                records = (List<ApplicationCore.DapperEntity.Products>)await _ProductsRepository.ShowByCatID(catid, userid);
            }

            return new ReadProductsResponse
            {
                Products = records,
                Categories = categories
            };
            //Products = records,
            //Categories = (List<ApplicationCore.DapperEntity.Category>)categories

            // ViewBag.SelectedCategoryId = catid;
        }
    }

   
}
