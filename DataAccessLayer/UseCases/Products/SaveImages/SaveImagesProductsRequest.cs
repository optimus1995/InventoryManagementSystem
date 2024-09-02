using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.UseCases.Products.SaveImages
{
    public class SaveImagesProductRequest : IRequest<SaveImagesProductResponse>
    {

      
        public string ProductId { get; set; }

        public List<IFormFile> ImagePath { get; set; }
    }
}
//public class CreateProductsRequestValidator: AbstractValidator<CreateProductsRequest>
//{
//    public CreateProductsRequestValidator()
//    {
//        RuleFor(x=>x.Name).NotNull().MaximumLength(50);
//        RuleFor(x=>x.Description).NotNull().MaximumLength(50);
//        RuleFor(x => x.Price).NotNull().GreaterThan(0);
//        RuleFor(x => x.CategoryID).NotNull();
//        RuleFor(x => x.SKU).NotNull().GreaterThan(0);
//        RuleFor(x => x.quantity).NotNull().GreaterThan(0);

//    }
//}
