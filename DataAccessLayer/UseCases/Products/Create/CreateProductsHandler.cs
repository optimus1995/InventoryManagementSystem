using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.Identity.Client;
namespace ApplicationCore.UseCases.Products.Create
{
    public class CreateProductsHandler : IRequestHandler<CreateProductsRequest, CreateProductsResponse>
    //    {
    //    }
    //}
    {
        private readonly IProductsRepository _productsRepository;

        private readonly ICategoryRepository _categoryRepository;

        public CreateProductsHandler(IProductsRepository ProductsRepository, ICategoryRepository categoryRepository)
        {

            _productsRepository = ProductsRepository;
            _categoryRepository = categoryRepository;

        }

        public async Task<CreateProductsResponse> Handle(CreateProductsRequest productData, CancellationToken cancellationToken)
        {
            //var categories = await _categoryRepository.GetAll();
            //var category = categories.Where(x => x.Id == productData.CategoryID);

           

            var Products = new ApplicationCore.DapperEntity.Products
            {
                Name = productData.Name,
                Description = productData.Description,
                SKU = productData.SKU,
                Price = productData.Price,
                quantity = productData.quantity,
                CreatedAt = DateTime.Now,
                CategoryID = productData.CategoryID,
                UserId= productData.UserId
                

                };
           var productcreated= _productsRepository.CreateProducts(Products);

            return new CreateProductsResponse
            {
                Id = productcreated.Id
                
            };

        }
    }

}