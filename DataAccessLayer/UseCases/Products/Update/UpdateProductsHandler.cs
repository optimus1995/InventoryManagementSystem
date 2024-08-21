using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.Identity.Client;
namespace ApplicationCore.UseCases.Products.Update
{
    public class UpdateProductsHandler : IRequestHandler<UpdateProductsRequest, UpdateProductsResponse>
    //    {
    //    }
    //}
    {
        private readonly IProductsRepository _productsRepository;

        private readonly ICategoryRepository _categoryRepository;

        public UpdateProductsHandler(IProductsRepository ProductsRepository, ICategoryRepository categoryRepository)
        {

            _productsRepository = ProductsRepository;
            _categoryRepository = categoryRepository;

        }

        public async Task<UpdateProductsResponse> Handle(UpdateProductsRequest productData, CancellationToken cancellationToken)
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
                CategoryID = productData.CategoryID,
                UserId= productData.UserId,
                Id = productData.Id
                

                };
           var productUpdated= _productsRepository.UpdateProducts(Products);

            return new UpdateProductsResponse
            {
                Id = productUpdated.Id
                
            };

        }
    }

}