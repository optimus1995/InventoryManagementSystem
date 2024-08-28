using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Category.CreateCategory;
using ApplicationCore.UseCases.Customers.CreateCustomers;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;


namespace ApplicationCore.UseCases.Customers.CreateCustomers
{
    public class SaveCustomersRequest : IRequest<SaveCustomersResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public AspNetUsers? AspNetUsers { get; set; }
    }
}


public class SaveCustomerValidator : AbstractValidator<SaveCustomersRequest>
{
    public SaveCustomerValidator()
    {
        RuleFor(rule => rule.Name).NotNull().MaximumLength(50);
        RuleFor(rule => rule.Email).NotNull().EmailAddress();
        RuleFor(rule => rule.BillingAddress).NotNull();
        RuleFor(rule => rule.ShippingAddress).NotNull();

    }

}