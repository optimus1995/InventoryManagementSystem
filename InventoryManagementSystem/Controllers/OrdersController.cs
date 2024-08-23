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
using System.Threading;
using MediatR;
using ApplicationCore.UseCases.Orders.Read;
using ApplicationCore.UseCases.Orders.Create;
using ApplicationCore.UseCases.Orders.GetBarChart;
using ApplicationCore.UseCases.Orders.GetGraphChart;

namespace InventoryManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly ICategoryRepository _categoryRepository;
        private LanguageServices _languageServices;
        private readonly IMediator  _mediator;
        public OrdersController(LanguageServices languageServices, ICategoryRepository categoryRepository ,ICustomersRepository customersRepository, IOrdersRepository ordersRepository , IProductsRepository productsRepository, IMediator mediator)
        {   _languageServices = languageServices;
            _categoryRepository = categoryRepository;
            _customersRepository = customersRepository; 
            _ordersRepository = ordersRepository;
             _productsRepository = productsRepository;
            _mediator = mediator;
        }


        public IActionResult Index( )
        {

            return View();
        }

        [Route("Orders/Result")]
        [HttpGet]
        //done 
        public async Task<IActionResult> Result(CancellationToken cancellationToken)
        {
            var request = new ReadOrdersRequest();
            var response = await _mediator.Send(request, cancellationToken);

            return View(response.Orders);
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

        //done
        public async Task<IActionResult> GetBarChartResult(BarChartRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return View(response);
        }




        public IActionResult SaveRecord()
        {
            return View();
        }

        [HttpGet]
        //done
        public async Task<IActionResult> SaveOrder()
        {
            var request = new CreateOrdersRequest();
            

            var response = await _mediator.Send(request);
            return View(response);
        }
        [HttpPost]
        
            public IActionResult SaveRecord(SaveOrdersRequest request, CancellationToken cancellationToken)
        {
           
            var response = _mediator.Send(request, cancellationToken);
            return RedirectToAction("Result");
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
        //done
        public async Task<IActionResult> BarGraphResult(GraphChartRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Json(response);

        }
    }
}

