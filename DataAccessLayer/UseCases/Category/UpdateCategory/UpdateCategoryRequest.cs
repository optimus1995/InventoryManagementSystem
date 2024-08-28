using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.UseCases.Category.CreateCategory;
using ApplicationCore.UseCases.Category.UpdateCategory;
using FluentValidation;
using MediatR;

namespace ApplicationCore.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(rule => rule.Name).NotNull().MaximumLength(50);
    }

}