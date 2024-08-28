using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Orders.CreateOrders;
using FluentValidation;
using MediatR;


namespace ApplicationCore.UseCases.Orders.CreateOrders
{
    public class CreateOrdersRequest : IRequest<CreateOrdersResponse>
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerBillingAddress { get; set; }
        public string? CustomerShippingAddress { get; set; }
        public int? ProductQuantity { get; set; }
        public List<ApplicationCore.DapperEntity.Customers> Customers { get; set; }
        public List<ApplicationCore.DapperEntity.Products> Products { get; set; }
        public List<ApplicationCore.DapperEntity.Category> Categories { get; set; }
    }
}

