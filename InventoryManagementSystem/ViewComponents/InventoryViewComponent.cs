using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
namespace InventoryManagementSystem.ViewComponents
{
    public class InventoryViewComponent : ViewComponent
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoryRepository _categoryRepository;

        public InventoryViewComponent(IProductsRepository productsRepository, ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
            _productsRepository = productsRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var (productCount, categoryCount) = await _productsRepository.GetCount();

            var counts = new Counts
            {
                ProductCount = productCount,
                CategoryCount = categoryCount
            };

            return View(counts);
        }


    }
}
