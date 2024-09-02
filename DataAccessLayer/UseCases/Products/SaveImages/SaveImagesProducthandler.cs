using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
namespace ApplicationCore.UseCases.Products.SaveImages
{
    public class SaveImagesProductHandler : IRequestHandler<SaveImagesProductRequest, SaveImagesProductResponse>
    //    {
    //    }
    //}
    {
        private readonly IProductsRepository _productsRepository;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mappper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SaveImagesProductHandler(IProductsRepository ProductsRepository,
            ICategoryRepository categoryRepository, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment
                                            )
        {

            _productsRepository = ProductsRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
             _mappper = mapper; 
            _hostingEnvironment = webHostEnvironment;

        }

        //    public async Task<SaveImagesProductResponse> Handle( SaveImagesProductRequest productData, CancellationToken cancellationToken)
        //    {
        //        var imagePath= productData.ImagePath;
        //        if (imagePath != null)
        //        {
        //            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
        //            if (!Directory.Exists(uploadsFolder))
        //            {
        //                Directory.CreateDirectory(uploadsFolder);
        //            }

        //            foreach (var file in imagePath)
        //            {
        //                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //                var filePath = Path.Combine(uploadsFolder, fileName);


        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }

        //                var productimage = new ProductImages
        //                {
        //                    ProductId = productData.ProductId,
        //                    ImagePath = filePath,

        //                    //       imagepath = fileName
        //                };
        //                var result = _productsRepository.SaveImages(productimage);
        //                //Save to database(use your repository or DbContext here)
        //                //_context.ProductImages.Add(productImage);
        //                //await _context.SaveChangesAsync();
        //            }


        //         //   var result = _productsRepository.SaveImages(productimage);
        //            var SaveImageProductResponse = new SaveImagesProductResponse();


        //            return SaveImageProductResponse;
        //        }













        //        //var productcreated = _productsRepository.CreateProducts(productData);


        //        return new SaveImagesProductResponse
        //        {
        //            Id =  Convert.ToInt32( productData.ProductId)

        //        };

        //    }


        public async Task<SaveImagesProductResponse> Handle(SaveImagesProductRequest productData, CancellationToken cancellationToken)
        {
            var imagePath = productData.ImagePath;
            if (imagePath != null)
            {
                 var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");

        // Ensure the directory exists
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        foreach (var file in imagePath)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var relativePath = Path.Combine("Uploads", fileName);

                    var productImage = new ProductImages
                    {
                        ProductId = productData.ProductId,
                        ImagesPath = relativePath // Store the relative path
                    };

                    // Await the SaveImages call
                    var result = await _productsRepository.SaveImages(productImage);

                    // Handle the result if needed
                }

                var saveImageProductResponse = new SaveImagesProductResponse
                {
                    Id = Convert.ToInt32(productData.ProductId)
                };

                return saveImageProductResponse;
            }

            return new SaveImagesProductResponse
            {
                Id = Convert.ToInt32(productData.ProductId)
            };
        }
    }

}