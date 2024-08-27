﻿using Microsoft.AspNetCore.Authorization;
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
using ApplicationCore.UseCases.Products.CreateProducts;
using ApplicationCore.UseCases.Products.UpdateProducts;
using ApplicationCore.UseCases.Products.ReadProducts;
using DocumentFormat.OpenXml.Spreadsheet;
using ApplicationCore.UseCases.Category.ReadCategory;
using System.Threading;
using ApplicationCore.UseCases.Products.GetGraphChart;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private IStringLocalizer<ProductsController> _stringLocalizer;

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, IStringLocalizer<ProductsController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            _mediator = mediator;
        }

        //done
        [Route("Products/Result")]
        [HttpGet]
        public async Task<IActionResult> Result(int catid, CancellationToken cancellationToken)
        {
            try
            {
                var request = new ReadProductsRequest();
                request.catid = catid;
                var s = await _mediator.Send(request, cancellationToken);
                ViewBag.SelectedCategoryId = catid;

                return View(s);


                //return View(viewModel);




            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("Products/SaveProduct")]
        [HttpGet]
        //done
        public async Task<IActionResult> SaveProduct()
        {
            var request = new ReadCategoryRequest();
            var cancellationToken = new CancellationToken();

            var records = await _mediator.Send(request, cancellationToken);
            var categories = records;

            return View(categories);
        }
        [HttpPost]
        //done
        public async Task<IActionResult> SaveProduct(CreateProductsRequest productData, CancellationToken cancellation)
        {
            try
            {
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
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var catrequest = new ReadCategoryRequest();
            ViewBag.Category = await _mediator.Send(catrequest, cancellationToken);
            var productrequest = new GetProductsRequest();
            productrequest.Id = id;
            var product = await _mediator.Send(productrequest, cancellationToken);
            if (product == null)
            {
                return NotFound();
            }

            return View(product.products);
        }

        //done
        public async Task<IActionResult> EditAsync(UpdateProductsRequest productData, CancellationToken cancellationToken)
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

        //public IActionResult Delete(int id)
        //{
        //    _productsRepository.DeleteRecord(id);
        //    return RedirectToAction("Result");

        //}
        //done
        public async Task<IActionResult> ExportRecord(ReadProductsRequest readProductsRequest, CancellationToken cancellation)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var id = identity.FindFirst(ClaimTypes.NameIdentifier);
                string userId = id.Value;

                var records = await _mediator.Send(readProductsRequest, cancellation);
                var recordsList = records;

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
                    for (int i = 0; i < recordsList.Products.Count; i++)
                    {
                        var record = recordsList.Products[i];
                        worksheet.Cell(i + 2, 1).Value = record.Id;
                        worksheet.Cell(i + 2, 2).Value = record.Name;
                        worksheet.Cell(i + 2, 3).Value = record.Description;
                        worksheet.Cell(i + 2, 4).Value = record.SKU;
                        worksheet.Cell(i + 2, 5).Value = record.Price;
                        worksheet.Cell(i + 2, 6).Value = record.quantity;
                        worksheet.Cell(i + 2, 7).Value = record.CreatedAt.ToString(format: "dddd, dd MMMM yyyy  hh:mm tt ");

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


        //[Route("Products/ShowProducts")]
        //[HttpGet]
        //public async Task<IActionResult> ShowProducts(int id)
        //{
        //    try
        //    {
        //        var i = (ClaimsIdentity)User.Identity;
        //        var uid = i.FindFirst(ClaimTypes.NameIdentifier);
        //        string userid = uid.Value;


        //        ViewBag.SelectedCategoryId = id;
        //        var records = await _productsRepository.ShowByCatID(id, userid);
        //        return View(records);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpGet]
        //done
        public async Task<IActionResult> ResultName()
        {
            var article = _stringLocalizer["Article"];

            return Ok(article.Value);

        }
        public async Task<IActionResult> GraphResult(GraphChartRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            // var result = response.barChartOrders;
            var result = response.productCategoryGraphs;
            return Json(result);
        }
        public async Task<IActionResult> Delete(int id)
        {
            //        var request = new DeleteCategoryRequest { Id = id };
            await _mediator.Send(id);

            return RedirectToAction("Result");
        }
    }


}
