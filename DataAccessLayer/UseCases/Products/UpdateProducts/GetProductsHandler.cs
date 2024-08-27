using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.Identity.Client;
using Azure.Core;
using ApplicationCore.UseCases.Employee.UpdateRoleEmployee;
namespace ApplicationCore.UseCases.Products.UpdateProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    //    {
    //    }
    //}
    {
        private readonly IProductsRepository _productsRepository;

        private readonly ICategoryRepository _categoryRepository;

        public GetProductsHandler(IProductsRepository ProductsRepository, ICategoryRepository categoryRepository)
        {

            _productsRepository = ProductsRepository;
            _categoryRepository = categoryRepository;

        }

        public async Task<GetProductsResponse> Handle(GetProductsRequest productData, CancellationToken cancellationToken)
        {
            //var categories = await _categoryRepository.GetAll();
            //var category = categories.Where(x => x.Id == productData.CategoryID);

           

            var id = productData.Id;
            var products = await _productsRepository.GetrecordforUpdate(id);

            var prod = new GetProductsResponse
            {
                products = products
            };
            return prod;
        }
    }

}