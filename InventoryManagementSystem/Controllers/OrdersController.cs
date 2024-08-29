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
using ApplicationCore.UseCases.Orders.ReadOrders;
using ApplicationCore.UseCases.Orders.CreateOrders;
using ApplicationCore.UseCases.Orders.GetBarChart;
using ApplicationCore.UseCases.Products.GetGraphChart;
using Microsoft.AspNetCore.Http.HttpResults;
using ApplicationCore.UseCases.Orders.SpecificOrder;
using ApplicationCore.UseCases.Products.ReadProducts;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using FluentValidation;
using DocumentFormat.OpenXml.Presentation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private LanguageServices _languageServices;
        private readonly IMediator _mediator;
        private readonly IValidator<SaveOrdersRequest> _validator;
        public OrdersController(LanguageServices languageServices, IMediator mediator,
            IValidator <SaveOrdersRequest> validator)
        {

            _mediator = mediator;
            _languageServices = languageServices;
            _validator = validator;
        }


        public IActionResult Index()
        {

            return View();
        }

        [Route("Orders/Result")]
        [HttpGet]
        //done 
        public async Task<IActionResult> Result()
        {
            var cancellationToken = new CancellationToken();
            var request = new ReadOrdersRequest();
            var response = await _mediator.Send(request, cancellationToken);

            return View(response.Orders);
        }

        [Route("Orders/Details")]
        [HttpGet]
        public async Task<IActionResult> Details(int Orderid, CancellationToken cancellationToken)
        {
            var request = new SpecificOrdersRequest();
            request.Id = Orderid;
            var response = await _mediator.Send(request, cancellationToken);
            Console.WriteLine(response);
            var result = response.Orders;
            //var json = JsonSerializer.Serialize(response);
            return View(result);


        }

        //public async Task<IActionResult> GetAll()
        //{
        //    var s = await _ordersRepository.Result();
        //    return View(s);
        //}

        //done
        public async Task<IActionResult> GetBarChartResult( CancellationToken cancellationToken)
        {
            var request = new BarChartRequest();
            var response = await _mediator.Send(request, cancellationToken);
            var result = response.barChartOrders;
            return Json(result);

        }
        [HttpGet]
        //done
        public async Task<IActionResult> SaveOrder()
        {
            var request = new CreateOrdersRequest();
            var canelation = new CancellationToken();
            var response = await _mediator.Send(request, canelation);
            return View(response);
        }
        [HttpPost]

        public async Task<IActionResult> SaveRecord(SaveOrdersRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (result.Errors.Any())
            {
                var errorList = result.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage
                }).ToList();

                return Json(new { success = false, errors = errorList });
            }

            var cancellationToken = new CancellationToken();
            var response = await _mediator.Send(request, cancellationToken);

            return Json(new { success = true, message = "Order saved successfully", data = response });
        }


        public async Task<IActionResult> ProductByCategory(ReadProductsRequest request,CancellationToken cancellationToken )
        {
    //        int id = Convert.ToInt32(Catid);

            try
            {
             



                var categoryrecords = await  _mediator.Send(request,cancellationToken);
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

