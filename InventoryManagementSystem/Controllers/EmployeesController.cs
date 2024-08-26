using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using ApplicationCore.Enum;
using ApplicationCore.UseCases.Employee.Read;
using ApplicationCore.UseCases.Employee.UpdateRole;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "ADMIN,Admin,  SUPERADMIN, SuperAdmin")]
        //done
        public async Task<IActionResult> Index(ReadEmployeesRequest request, CancellationToken cancellationToken)
        {
            var s = await _mediator.Send(request, cancellationToken);
            // var s = await _EmployeesRepository.GetAll();
            return View(s);
        }
        //done
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RoleUpdate(string UserId, CancellationToken cancellationToken)
        {
            // ViewBag.Roles = await _EmployeesRepository.GetAllRoles();

            var getrolerequest = new GetRoleRequest();
            getrolerequest.Id = UserId;
            var viewModel = await _mediator.Send(getrolerequest, cancellationToken);
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        //done
        public IActionResult RoleUpdate(UpdateRoleRequest updaterolerequest, CancellationToken cancellationToken)
        {
            try
            {
                var viewModel = _mediator.Send(updaterolerequest, cancellationToken);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
