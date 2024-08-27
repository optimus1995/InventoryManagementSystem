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
using ApplicationCore.UseCases.Customers.CreateCustomers;
using ApplicationCore.UseCases.Customers.UpdateCustomers;
using ApplicationCore.UseCases.Customers.ReadCustomers;
using ApplicationCore.UseCases.Customers.DeleteCustomers;
namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Result(ReadCustomersRequest request, CancellationToken cancellation)
        {
            var records = await _mediator.Send(request, cancellation);

            return View(records);
        }
        public IActionResult SaveRecord()
        {
            return View();
        }

        [HttpPost]
        [Route("Customers/SaveRecord")]
        public IActionResult SaveProduct(SaveCustomersRequest customerData, CancellationToken cancellationToken)
        {
            var s = _mediator.Send(customerData, cancellationToken);
            return RedirectToAction("Result");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(FetchCustomersRequest request, CancellationToken cancellationToken)
        {

            var customer = await _mediator.Send(request, cancellationToken);
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

        public IActionResult Delete(int id, CancellationToken cancellationToken)
        {
            //_customersRepository.DeleteRecord(id);
            var request = new DeleteCustomersRequest();
            request.Id = id;
            var result = _mediator.Send(request, cancellationToken);
            return RedirectToAction("Result");

        }

    }
}
