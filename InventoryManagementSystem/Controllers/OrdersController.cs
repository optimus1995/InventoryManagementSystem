using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Localization;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Text.Json;
using Infrastructure.Services;

namespace InventoryManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly ICategoryRepository _categoryRepository;
        private LanguageServices _languageServices;
        public OrdersController(LanguageServices languageServices, ICategoryRepository categoryRepository ,ICustomersRepository customersRepository, IOrdersRepository ordersRepository , IProductsRepository productsRepository)
        {   _languageServices = languageServices;
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
            

           
            //get culture information
         //   var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
        var s = await _ordersRepository.Result();
            Console.WriteLine(s);
            return View(s);
        }
       

        [Route("Orders/Details")]
        [HttpGet]
        public async Task<IActionResult> Details(int Orderid)
        {

            var response = await _ordersRepository.ResultByOrderId(Orderid);
            Console.WriteLine(response);
            //var json = JsonSerializer.Serialize(response);
            return View(response);


        }

        public async Task<IActionResult> GetAll()
        {
            var s = await _ordersRepository.Result();
            return View(s);
        }

        public async Task<IActionResult> GetBarChartResult()
        {
            var s = await _ordersRepository.BarChartOrderDetail();
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
            public IActionResult SaveRecord(Orders orders)
        {
            try
            {
                var i = (ClaimsIdentity)User.Identity;
                var id = i.FindFirst(ClaimTypes.NameIdentifier);
                var userid = id.Value;

                if (ModelState.IsValid)
                {
                    orders.OrderStatus = "Processing";
                    orders.IsActive = true;
                    orders.CreatedAt = DateTime.Now;
                    orders.CreatedBy = userid;
                    _ordersRepository.CreateOrders(orders);
                }
                else
                { return BadRequest(ModelState);
                }
                return Json("");
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
                return Json(categoryrecords);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
            return Json("");
        }


        public async Task<IActionResult> BarGraphResult()
        {
            try
            {
                var record = await _ordersRepository.BarChartOrderDetail();

                return Json(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }













    }
}
