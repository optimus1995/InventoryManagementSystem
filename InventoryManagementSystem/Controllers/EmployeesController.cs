using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using ApplicationCore.Enum;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagementSystem.Controllers
{
    public class EmployeesController : Controller

    {
        private readonly IEmployeesRepository _EmployeesRepository;

        public EmployeesController( IEmployeesRepository employeesRepository)
        {
             
            _EmployeesRepository = employeesRepository;
            _EmployeesRepository = employeesRepository;
        }
        [Authorize(Roles = "ADMIN,Admin,  SUPERADMIN, SuperAdmin")]
        public async  Task <IActionResult> Index()
        {
            var s = await _EmployeesRepository.GetAll();
            return View(s);
        }
        //public async Task<IActionResult> RoleUpdate(string userid)
        //{
        //    var role = await _EmployeesRepository.GetRoleData(userid);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    var s = role.ToList();
        //    ViewBag.UserName = s;
        //    // Assuming UserName is a property of AspNetRoles
        //    return View(role);
        //}

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RoleUpdate(string userid)
        {
            try
            {
                ViewBag.Roles = await _EmployeesRepository.GetAllRoles();

                var role =await  _EmployeesRepository.GetRoleData(userid);

                var viewModel = new AspNetUserRoleAssigned
                {
                    UserId = role.Id,
                    UserName = role.UserName,
                    RoleId = role.AspNetUserRoles.RoleId,
                    RoleName = role.AspNetRoles.NormalizedName,
                };
                return View(viewModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult RoleUpdate(AspNetUserRoles AspUserRoles)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var AspNetUserRoles = new AspNetUserRoles
                    {
                        RoleId = AspUserRoles.RoleId,
                        UserId = AspUserRoles.UserId                    };
                    _EmployeesRepository.UpdateRoles(AspNetUserRoles);
                    return RedirectToAction("Index");
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







    }
}
