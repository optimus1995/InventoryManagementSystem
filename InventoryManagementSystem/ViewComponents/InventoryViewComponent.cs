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
        private readonly ICustomersRepository _customersRepository;
        private  readonly IOrdersRepository _ordersRepository;


        public InventoryViewComponent(IProductsRepository productsRepository, ICategoryRepository categoryRepository , ICustomersRepository customersRepository, IOrdersRepository ordersRepository) {
            _categoryRepository = categoryRepository;
            _productsRepository = productsRepository;
            _customersRepository = customersRepository;
            _ordersRepository = ordersRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var (productCount, categoryCount) = await _productsRepository.GetCount();
            var (customersCount, ordersCount) = await _ordersRepository.GetCount();

            var counts = new Counts
            {
                ProductCount = productCount,
                CategoryCount = categoryCount,
                CustomersCount = customersCount,
                OrdersCount = ordersCount
            };
            return View(counts);
        }


    }
}
