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
using Cqrs.Events;
using FluentValidation;
namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<SaveCustomersRequest> _createvalidator;
        private readonly IValidator<UpdateCustomersRequest> _updatevalidator;

        public CustomersController(IMediator mediator, IValidator<SaveCustomersRequest> createvalidator, IValidator<UpdateCustomersRequest> updatevalidator)
        {
            _mediator = mediator;
            _createvalidator = createvalidator;
            _updatevalidator = updatevalidator;

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
        public async Task<IActionResult> SaveProduct(SaveCustomersRequest customerData, CancellationToken cancellationToken)
        {
            var result = await _createvalidator.ValidateAsync(customerData);

            // If validation fails, add the errors to ModelState
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(customerData); // Return the view with the command object to display errors
            }

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

        public async Task<IActionResult> Edit(UpdateCustomersRequest customerData, CancellationToken cancellationToken)
        {
            var result = await _updatevalidator.ValidateAsync(customerData);

            // If validation fails, add the errors to ModelState
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(customerData); // Return the view with the command object to display errors
            }

            var res = _mediator.Send(customerData, cancellationToken);
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
