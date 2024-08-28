using ApplicationCore.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.Identity.Client;
using AutoMapper;
namespace ApplicationCore.UseCases.Products.UpdateProducts
{
    public class UpdateProductsHandler : IRequestHandler<UpdateProductsRequest, UpdateProductsResponse>
    //    {
    //    }
    //}
    {
        private readonly IProductsRepository _productsRepository;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateProductsHandler(IProductsRepository ProductsRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper )
        {

            _productsRepository = ProductsRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }

        public async Task<UpdateProductsResponse> Handle(UpdateProductsRequest productData, CancellationToken cancellationToken)
        {
            //var categories = await _categoryRepository.GetAll();
            //var category = categories.Where(x => x.Id == productData.CategoryID);

           

           
            var Products = _mapper.Map<DapperEntity.Products>(productData);

            var productUpdated = _productsRepository.UpdateProducts(Products);

            return new UpdateProductsResponse
            {
                Id = productUpdated.Id
                
            };

        }
    }

}