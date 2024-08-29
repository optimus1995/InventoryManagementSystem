using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Orders.CreateOrders;
using MediatR;
using Microsoft.AspNetCore.Identity;
using FluentValidation;


namespace ApplicationCore.UseCases.Orders.CreateOrders
{
    public class SaveOrdersRequest : IRequest<SaveOrdersResponse>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TotalPrice { get; set; }

        public int Discount { get; set; }
        public int TotalAmount { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? OrderStatus { get; set; }
        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }
        public List<OrderDetails> ListDetails { get; set; } = new List<OrderDetails>();

        public string? isActive { get; set; }
    }
}


public class SaveOrderRequestValidator : AbstractValidator<SaveOrdersRequest>
{
    public SaveOrderRequestValidator()
    {
        RuleFor(rule => rule.Discount).NotNull();
        RuleFor(rule => rule.CustomerId).NotNull();
        RuleFor(rule => rule.TotalAmount).NotNull();
        RuleFor(rule => rule.ListDetails)
            .Must(list => list != null && list.Count >= 1)
            .WithMessage("Product Table must contain at least 1 item.");
        RuleFor(rule => rule.Discount).NotNull().GreaterThan(0).LessThanOrEqualTo(100);
    }

}