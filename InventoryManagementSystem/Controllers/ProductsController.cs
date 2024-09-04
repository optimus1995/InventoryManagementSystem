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
using ApplicationCore.UseCases.Products.CreateProducts;
using ApplicationCore.UseCases.Products.UpdateProducts;
using ApplicationCore.UseCases.Products.ReadProducts;
using DocumentFormat.OpenXml.Spreadsheet;
using ApplicationCore.UseCases.Category.ReadCategory;
using System.Threading;
using ApplicationCore.UseCases.Products.GetGraphChart;
using FluentValidation;
using DocumentFormat.OpenXml.Office2010.Excel;
using NUglify.Helpers;
using ApplicationCore.UseCases.Products.SaveImages;
using ApplicationCore.UseCases.Products.DisplayImages;
using System.IO.Compression;
using NuGet.Protocol.Plugins;
using System.IO;
using NUglify.JavaScript.Syntax;

namespace InventoryManagementSystem.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private IStringLocalizer<ProductsController> _stringLocalizer;

        private readonly IMediator _mediator;
        private readonly IValidator<CreateProductsRequest> _validator;
        private readonly IValidator<UpdateProductsRequest> _updatevalidator;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public ProductsController(IMediator mediator, IStringLocalizer<ProductsController> stringLocalizer,
            IValidator<CreateProductsRequest> validator, IValidator<UpdateProductsRequest> updatevalidator,
            IWebHostEnvironment hostingEnvironment)
        {
            _stringLocalizer = stringLocalizer;

            _mediator = mediator;
            _validator = validator;
            _updatevalidator = updatevalidator;
            _hostingEnvironment = hostingEnvironment;
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
            ViewBag.Categories = categories.Categories;

            return View();
        }
        [HttpPost]
        //done
        public async Task<IActionResult> SaveProduct(CreateProductsRequest productData, CancellationToken cancellation)
        {
            var result = await _validator.ValidateAsync(productData);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(productData); // Return the view with the validation errors
            }
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
            //    var updateProductRequest = _mapper.Map<UpdateProductsRequest>(product);

            return View();
        }

        //done
        public async Task<IActionResult> EditAsync(UpdateProductsRequest productData, CancellationToken cancellationToken)
        {
            var result = await _updatevalidator.ValidateAsync(productData);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(productData); // Return the view with the validation errors
            }

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
        // [Route("Products/{id}/AddImages")]
        [HttpGet]
        //    [Route("Products/AddorEditImages/{id}")]
        public async Task<IActionResult> AddorEditImages(int id, CancellationToken cancellationToken)
        {
            var productrequest = new GetProductsRequest();
            productrequest.Id = id;
            var product = await _mediator.Send(productrequest, cancellationToken);
            ViewBag.Product = product.products;
            if (product == null)
            {

            }
            //    var updateProductRequest = _mapper.Map<UpdateProductsRequest>(product);


            return View();
        }


        [HttpPost]
        //        public async Task<IActionResult> AddorEditImages(int productId, List <IFormFile> ImagePath, CancellationToken cancellationToken)
        public async Task<IActionResult> AddorEditImages(SaveImagesProductRequest request, CancellationToken cancellationToken)

        {
            var result = await _mediator.Send(request, cancellationToken);

            //if (ImagePath != null)
            //{
            //    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
            //    if (!Directory.Exists(uploadsFolder))
            //    {
            //        Directory.CreateDirectory(uploadsFolder);
            //    }

            //    foreach (var file in ImagePath)
            //    {
            //            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            //            var filePath = Path.Combine(uploadsFolder, fileName);

            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                await file.CopyToAsync(stream);
            //            }

            //        var productimage = new productimage
            //        {
            //            productid = productid,
            //            imagename = filename,
            //            imagepath = filepath
            //        };

            //        Save to database(use your repository or DbContext here)
            //        _context.ProductImages.Add(productImage);
            //        await _context.SaveChangesAsync();
            //    }


            //    return RedirectToAction("ProductDetails", new { id = productId });
            //}

            return RedirectToAction("Result");
        }


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

        public async Task<IActionResult> ShowImages(DisplayImagesRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            ViewBag.productname = response.productsImage;
            ViewBag.ProductId=response.ProductId;
            return View(response);
        }

        //public async Task<IActionResult> DownloadAllImages(DisplayImagesRequest request, CancellationToken cancellationToken)
        //{
        //    var response = await _mediator.Send(request, cancellationToken);
        //    var productImages = response.productsImage;

        //    var zipName = $"{productImages.FirstOrDefault()?.ProductName}_{DateTime.Now:yyyyMMddHHmmss}.zip";

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
        //        {
        //            foreach (var item in productImages)
        //            {
        //                var fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), item.ImagesPath);

        //                if (!string.IsNullOrEmpty(fullImagePath) && System.IO.File.Exists(fullImagePath))
        //                {
        //                    var entry = zip.CreateEntry(item.ImageName);

        //                    using (var fileStream = new FileStream(fullImagePath, FileMode.Open, FileAccess.Read))
        //                    using (var entryStream = entry.Open())
        //                    {
        //                        await fileStream.CopyToAsync(entryStream);
        //                    }
        //                }
        //            }
        //        }

        //        return File(ms.ToArray(), "application/zip", zipName);
        //    }
        //}

        public async Task<IActionResult> DownloadAllImages(DisplayImagesRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            var productImages = response.productsImage;
            

            var zipName = $"{productImages.FirstOrDefault()?.ProductName}_{DateTime.Now:yyyyMMddHHmmss}.zip";

            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        foreach (var item in productImages)
                        {
                            // Use _hostingEnvironment.WebRootPath to access the wwwroot folder
                            var fullImagePath = Path.Combine(_hostingEnvironment.WebRootPath, item.ImagesPath.Replace("/", "\\"));

                            if (System.IO.File.Exists(fullImagePath))
                            {
                                var entry = zip.CreateEntry(item.ImageName + Path.GetExtension(item.ImagesPath));


                                using (var fileStream = new FileStream(fullImagePath, FileMode.Open, FileAccess.Read))
                                using (var entryStream = entry.Open())
                                {
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"File not found: {fullImagePath}");
                            }
                        }
                    }

                    // Reset the MemoryStream position to the beginning before returning the file.
                    ms.Seek(0, SeekOrigin.Begin);
                    return File(ms.ToArray(), "application/zip", zipName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View();        }


    }


}
