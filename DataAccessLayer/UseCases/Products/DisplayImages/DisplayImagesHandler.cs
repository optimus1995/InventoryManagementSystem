using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System.Diagnostics;
using System.Collections.Generic;


namespace ApplicationCore.UseCases.Products.DisplayImages
{
    public class DisplayImagesHandler : IRequestHandler<DisplayImagesRequest, DisplayImagesResponse>
    {
        private readonly IProductsRepository _ProductsRepository;
        private readonly IMapper _mapper;

        public DisplayImagesHandler(IProductsRepository ProductsRepository,
            IMapper mapper
            )

        {

            _ProductsRepository = ProductsRepository;
            _mapper = mapper;

        }

        public async Task<DisplayImagesResponse> Handle(DisplayImagesRequest request, CancellationToken cancellationToken)
        {
            
            var result = await _ProductsRepository.DisplayImages(request.Id);


            var imagesList = result.ToList();

            // Return the response
            return new DisplayImagesResponse
            {
                
                productsImage = imagesList
            };

        }
    }

   
}
