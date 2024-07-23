using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using System.Security.Claims;

using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace InventoryManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly ICategoryRepository _categoryRepository;
        public OrdersController(ICategoryRepository categoryRepository ,ICustomersRepository customersRepository, IOrdersRepository ordersRepository , IProductsRepository productsRepository)
        { 
            _categoryRepository = categoryRepository;
            _customersRepository = customersRepository; 
            _ordersRepository = ordersRepository;
             _productsRepository = productsRepository;
        }


        public IActionResult Index( )
        {
            return View();
        }

        [Route("Orders/Result")]
        [HttpGet]
        public async Task<IActionResult> Result() {
        
        var s = await _ordersRepository.Result();
            Console.WriteLine(s);
            return View(s);
        
        
        }
       

        public IActionResult SaveRecord()
        {
            return View();
        }

        [HttpGet]
       
        public async Task< IActionResult> SaveOrder()

        {
            
            var i = (ClaimsIdentity)User.Identity;
            var id = i.FindFirst(ClaimTypes.NameIdentifier);
            var userid = id.Value;
            var customerid = 1;
            var customer = await _customersRepository.GetrecordforUpdate(customerid);
            var products = (await _productsRepository.GetAll(userid)).ToList() ;
            var customers = (await _customersRepository.GetAll(userid)).ToList();
            var category = (await _categoryRepository.GetAll()).ToList();

            Console.WriteLine(products);
            var model = new CustomerProductView
            {
                CustomerId = customer.Id,
                CustomerBillingAddress = customer.BillingAddress,
                CustomerShippingAddress = customer.ShippingAddress,
                CustomerName = customer.Name,
                Products = products,
                Customers = customers,
                Categories= category
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult SaveOrder(OrderDetails orders) {
            try
            {
                var i = (ClaimsIdentity)User.Identity;
                var id = i.FindFirst(ClaimTypes.NameIdentifier);
                var userid = id.Value;

                if (ModelState.IsValid)
                {
                    var orderDetails = new OrderDetails
                    {
                        TotalProductPrice = orders.TotalProductPrice,
                        ProductId = orders.ProductId,
                        ProductQuantity = orders.ProductQuantity,
                        TotalPrice = orders.TotalPrice,
                        Discount = orders.Discount,
                        TotalAmount = orders.TotalAmount,
                        CustomerId = orders.CustomerId,
                        ProductPrice = orders.ProductPrice,
                        OrderStatus ="Processing",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userid

                        //  Category=productData.Category,
                    };
                    _ordersRepository.CreateOrders(orderDetails);
                    return RedirectToAction("Result");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }



        }

        public async Task <IActionResult> ProductByCategory(string Catid)
        {
            int id = Convert.ToInt32(Catid);

            try
            {
                var i = (ClaimsIdentity)User.Identity;
                var uid = i.FindFirst(ClaimTypes.NameIdentifier);
                string userid = uid.Value;



                var categoryrecords = await _productsRepository.ShowByCatID(id, userid);
                return View(categoryrecords);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return Json("");
        }

    }
}
