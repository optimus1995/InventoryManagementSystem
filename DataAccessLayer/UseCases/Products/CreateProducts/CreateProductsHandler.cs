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
namespace ApplicationCore.UseCases.Products.CreateProducts
{
    public class CreateProductsHandler : IRequestHandler<CreateProductsRequest, CreateProductsResponse>
    //    {
    //    }
    //}
    {
        private readonly IProductsRepository _productsRepository;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mappper;

        public CreateProductsHandler(IProductsRepository ProductsRepository,
            ICategoryRepository categoryRepository, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {

            _productsRepository = ProductsRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
             _mappper = mapper; 

        }

        public async Task<CreateProductsResponse> Handle(CreateProductsRequest productData, CancellationToken cancellationToken)
        {
            //var categories = await _categoryRepository.GetAll();
            //var category = categories.Where(x => x.Id == productData.CategoryID);
            var userid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            productData.UserId = userid;
            productData.CreatedAt = DateTime.Now;
            var Products = _mappper.Map<DapperEntity.Products>(productData);
            //var Products = new ApplicationCore.DapperEntity.Products
            //{
            //    Name = productData.Name,
            //    Description = productData.Description,
            //    SKU = productData.SKU,
            //    Price = productData.Price,
            //    quantity = productData.quantity,
            //    CreatedAt = DateTime.Now,
            //    CategoryID = productData.CategoryID,
            //    UserId= userid
                

            //    };
           var productcreated= _productsRepository.CreateProducts(Products);

            return new CreateProductsResponse
            {
                Id = productcreated.Id
                
            };

        }
    }

}