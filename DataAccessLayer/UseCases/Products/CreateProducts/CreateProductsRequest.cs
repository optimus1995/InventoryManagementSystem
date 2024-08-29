using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Category.CreateCategory;
using ApplicationCore.UseCases.Products.CreateProducts;
using FluentValidation;
using MediatR;

namespace ApplicationCore.UseCases.Products.CreateProducts
{
    public class CreateProductsRequest : IRequest<CreateProductsResponse>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SKU { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public string? UserId { get; set; }
        public ApplicationCore.DapperEntity.Category? Category { get; set; }
        public ApplicationCore.DapperEntity.AspNetUsers? AspNetUsers { get; set; }
    }
}
public class CreateProductsRequestValidator: AbstractValidator<CreateProductsRequest>
{
    public CreateProductsRequestValidator()
    {
        RuleFor(x=>x.Name).NotNull().MaximumLength(50);
        RuleFor(x=>x.Description).NotNull().MaximumLength(50);
        RuleFor(x => x.Price).NotNull().GreaterThan(0);
        RuleFor(x => x.CategoryID).NotNull();
        RuleFor(x => x.SKU).NotNull().GreaterThan(0);
        RuleFor(x => x.quantity).NotNull().GreaterThan(0);

    }
}
