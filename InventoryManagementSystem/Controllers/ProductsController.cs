using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using System.Data;
using IronXL;
using Infrastructure.Repository;
using ClosedXML.Excel;
using Microsoft.Extensions.Localization;
using MediatR;
using ApplicationCore.UseCases.Products.Create;
using ApplicationCore.UseCases.Products.Update;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private IStringLocalizer<ProductsController> _stringLocalizer;
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _mediator;

        public ProductsController( IMediator mediator ,IProductsRepository productsRepository, ICategoryRepository categoryRepository, IStringLocalizer<ProductsController>  stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
            _mediator = mediator;
        }


        [Route("Products/Result")]
        [HttpGet]
        public async Task <IActionResult> Result(int catid)
        {
            try
            {


                var i = (ClaimsIdentity)User.Identity;
                var id = i.FindFirst(ClaimTypes.NameIdentifier);
                string userid = id.Value;

                var categories = await _categoryRepository.GetAll();
                List<Products> records;
                if (catid == null|| catid==0)
                {

                     records = (List<Products>)await _productsRepository.GetAll(userid);
                }
                else {
                     records = (List<Products>)await _productsRepository.ShowByCatID(catid, userid);
                }
                var viewModel = new ProductViewModel
                {
                    Products = records,
                    Categories = (List<Category>)categories
                };
                ViewBag.SelectedCategoryId = catid;
                return View(viewModel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        
        [Route("Products/ShowProducts")]
        [HttpGet]
        public async Task<IActionResult> ShowProducts(int id) 
        {
            try
            {
                var i = (ClaimsIdentity)User.Identity;
                var uid = i.FindFirst(ClaimTypes.NameIdentifier);
                string userid = uid.Value;


                ViewBag.SelectedCategoryId = id;
                var records = await _productsRepository.ShowByCatID(id, userid);
                return View(records);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
          [Route("Products/SaveProduct")]
        [HttpGet]
        public async Task<IActionResult> SaveProduct()
        {
            //  List<Category> categories = new List<Category>();
           
            var categories = await _categoryRepository.GetAllCategories ();
            
            Console.WriteLine(categories);

            return View(categories);
        }


        //public async Task<IActionResult> SaveProduct(Products productData, CancellationToken cancellation)
        //{
        //    try
        //    {

        //        var categories = await _categoryRepository.GetAll();
        //        var category = categories.Where(x => x.Id == productData.CategoryID);

        //        var i = (ClaimsIdentity)User.Identity;
        //        var id = i.FindFirst(ClaimTypes.NameIdentifier);
        //        var userid = id.Value;
        //        productData.UserId = userid;
        //        if (ModelState.IsValid)
        //        {

        //            var response = await _mediator.Send(productData, cancellation);
        //            return RedirectToAction("Result");
        //        }
        //        else
        //        {
        //            return BadRequest(ModelState);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpPost]
        //dones
        public async Task<IActionResult> SaveProduct(CreateProductsRequest productData, CancellationToken cancellation)
        {
            try
            {
                //var categories = await _categoryRepository.GetAll();
                //var category = categories.FirstOrDefault(x => x.Id == productData.CategoryID);
                //if (category == null)
                //{
                //    ModelState.AddModelError(nameof(productData.CategoryID), "Invalid Category ID");
                //    return BadRequest(ModelState);
                //}

                var identity = (ClaimsIdentity)User.Identity;
                var idClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (idClaim == null)
                {
                    return StatusCode(500, "User ID not found");
                }

                productData.UserId = idClaim.Value;

                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(productData, cancellation);
                    return RedirectToAction("Result");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library like Serilog)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Category = await _categoryRepository.GetAll();

            var product = await _productsRepository.GetrecordforUpdate(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        public async Task<IActionResult> EditAsync(UpdateProductsRequest productData,CancellationToken cancellationToken)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _mediator.Send(productData, cancellationToken);
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
            _productsRepository.DeleteRecord(id);
            return RedirectToAction("Result"); 
        
        }
        //public IActionResult ExportRecord() {
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("DataSet", typeof(string));
        //    dt.Rows.Add("ID");
        //    dt.Rows.Add("Name");
        //    dt.Rows.Add("Description");
        //    dt.Rows.Add("SKU");
        //    dt.Rows.Add("Price");
        //    dt.Rows.Add("Quantity");
        //    dt.Rows.Add("Created Date");

        //   WorkBook  wb = WorkBook.Create(ExcelFileFormat.XLS);
        //    WorkSheet ws = wb.DefaultWorkSheet;
        //    int rowCount = 1;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        ws["A" + (rowCount)].Value = row[0].ToString();
        //        rowCount++;
        //    }
        //    wb.SaveAs("datatable.xlsx");
        //    return RedirectToAction("Result");
        //}   

    
public async Task<IActionResult> ExportRecord()
    {
        try
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = identity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = id.Value;

            var records = await _productsRepository.GetAll(userId);
                var recordsList = records?.ToList() ?? new List<Products>();

                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Records");

               // These are headers for the excel files //
                    worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Description";
                worksheet.Cell(1, 4).Value = "SKU";
                worksheet.Cell(1, 5).Value = "Price";
                worksheet.Cell(1, 6).Value = "Quantity";
                worksheet.Cell(1, 7).Value = "Created Date";
                    // after header we use loops to store all data in file 
                    //first rows is header and i=0 so add 1 for header and 1 for next line
                for (int i = 0; i < recordsList.Count; i++)
                {
                    var record = recordsList[i];
                    worksheet.Cell(i + 2, 1).Value = record.Id;
                    worksheet.Cell(i + 2, 2).Value = record.Name;
                    worksheet.Cell(i + 2, 3).Value = record.Description;
                    worksheet.Cell(i + 2, 4).Value = record.SKU;
                    worksheet.Cell(i + 2, 5).Value = record.Price;
                    worksheet.Cell(i + 2, 6).Value = record.quantity;
                    worksheet.Cell(i + 2, 7).Value = record.CreatedAt .ToString(format: "dddd, dd MMMM yyyy  hh:mm tt ");
                        
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
 
                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ProductsRecord.xlsx");
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }




        [HttpGet]
        public async Task<IActionResult> ResultName()
        {
            var article = _stringLocalizer["Article"];

            return Ok(article.Value);

        }
        public async Task<IActionResult> GraphResult()
        {
            try
            {
                var record = await _productsRepository.GetCountforChart();

                return Json(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }


}
