using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Customers.CreateCustomers;
using ApplicationCore.UseCases.Customers.UpdateCustomers;
using FluentValidation;
using MediatR;


namespace ApplicationCore.UseCases.Customers.UpdateCustomers
{
    public class UpdateCustomersRequest : IRequest<UpdateCustomersResponse>
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
public class UpdateCustomerValidator : AbstractValidator<UpdateCustomersRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(rule => rule.Name).NotNull().MaximumLength(50);
        RuleFor(rule => rule.Email).NotNull().EmailAddress();
        RuleFor(rule => rule.BillingAddress).NotNull();
        RuleFor(rule => rule.ShippingAddress).NotNull();

    }

}