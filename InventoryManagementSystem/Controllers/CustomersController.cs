using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using ApplicationCore.UseCases.Customers.Create;
using ApplicationCore.UseCases.Customers.Update;
using ApplicationCore.UseCases.Customers.Read;
namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ICustomersRepository _customersRepository;
        private readonly IMediator _mediator;


        public CustomersController(ICustomersRepository customersRepository, IMediator mediator)
        {
            _customersRepository = customersRepository;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Customers/Result")]
  
        public async Task<IActionResult> Result(ReadCustomersRequest request, CancellationToken cancellation)
        {
                var records = await  _mediator.Send(request, cancellation);
                
            return View(records);

        }
        //[Route("Products/ShowProducts")]
        //[HttpGet]
        //public async Task<IActionResult> ShowProducts(int id)
        //{
        //    try
        //    {
        //        var i = (ClaimsIdentity)User.Identity;
        //        var uid = i.FindFirst(ClaimTypes.NameIdentifier);
        //        string userid = uid.Value;



        //        var records = await _productsRepository.ShowByCatID(id, userid);
        //        return View(records);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }



        //}




        public IActionResult SaveRecord()
        {
            return View();
        }

        [HttpPost]
        [Route("Customers/SaveRecord")]
        public IActionResult SaveProduct(SaveCustomersRequest customerData, CancellationToken cancellationToken)
        {
                        //  Category=productData.Category,

            var s =  _mediator.Send (customerData,cancellationToken);
                    return RedirectToAction("SaveRecord");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(FetchCustomersRequest request, CancellationToken cancellationToken)
        {
            
            var customer = await _mediator.Send(request , cancellationToken);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]

        public IActionResult Edit(UpdateCustomersRequest customerData, CancellationToken cancellationToken)
        {

            var result = _mediator.Send(customerData, cancellationToken);
            return RedirectToAction("Result");

        }

        public IActionResult Delete(int id)
        {
            _customersRepository.DeleteRecord(id);
            return RedirectToAction("Result");

        }

    }
}
