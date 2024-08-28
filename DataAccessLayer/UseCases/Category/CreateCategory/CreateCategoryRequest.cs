using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.UseCases.Category.CreateCategory;
using ApplicationCore.UseCases.Orders.CreateOrders;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace ApplicationCore.UseCases.Category.CreateCategory
{
    public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
    {
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(rule => rule.Name).NotNull().MaximumLength(50);
           }

}