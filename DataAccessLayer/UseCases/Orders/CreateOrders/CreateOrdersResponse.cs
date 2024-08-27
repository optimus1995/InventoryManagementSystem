using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Orders.CreateOrders
{ 
    public class CreateOrdersResponse
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
