using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{
    public class CustomerProductView
    {

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerBillingAddress { get; set; }
        public string? CustomerShippingAddress { get; set; }

        public int? ProductQuantity { get; set; }
        public List <Customers> Customers { get; set; }
        public List<Products> Products { get; set; }

        public List<Category>Categories { get; set; }
    }
}
