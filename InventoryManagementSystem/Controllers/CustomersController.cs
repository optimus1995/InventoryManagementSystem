using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
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

        public CustomersController(ICustomersRepository customersRepository )
        {
            _customersRepository = customersRepository;
        }

        [HttpGet]
        [Route("Customers/Result")]
  
        public async Task<IActionResult> Result()
        {
            try
            {
                var i = (ClaimsIdentity)User.Identity;
                var id = i.FindFirst(ClaimTypes.NameIdentifier);
                string userid = id.Value;



                var records = await _customersRepository.GetAll(userid);
                return View(records);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
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
        public IActionResult SaveProduct(Customers customerData)
        {
            try
            {
               
                var i = (ClaimsIdentity)User.Identity;
                var id = i.FindFirst(ClaimTypes.NameIdentifier);
                var userid = id.Value;

                if (ModelState.IsValid)
                {
                    var Customers = new Customers
                    {
                        Name = customerData.Name,
                        Email = customerData.Email,
                        BillingAddress = customerData.BillingAddress,
                        ShippingAddress = customerData.ShippingAddress,
                        
                        CreatedAt = DateTime.Now,
                        CreatedBy = userid

                        //  Category=productData.Category,
                    };
                     _customersRepository.CreateRecord(Customers);
                    return RedirectToAction("SaveRecord");
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var customer = await _customersRepository.GetrecordforUpdate(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]

        public IActionResult Edit(Customers customerData)
        {
            var i = (ClaimsIdentity)User.Identity;
            var id = i.FindFirst(ClaimTypes.NameIdentifier);
            var userid = id.Value;

            try
            {
                if (ModelState.IsValid)
                {
                    var Customers = new Customers
                    {
                        Id = customerData.Id,
                        Name = customerData.Name,
                        Email = customerData.Email,
                        BillingAddress = customerData.BillingAddress,
                        ShippingAddress = customerData.ShippingAddress,

                        UpdatedAt = DateTime.Now,
                        UpdatedBy = userid

                        //  Category=productData.Category,
                    };
                    _customersRepository.Update(Customers);
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

        public IActionResult Delete(int id)
        {
            _customersRepository.DeleteRecord(id);
            return RedirectToAction("Result");

        }

    }
}
