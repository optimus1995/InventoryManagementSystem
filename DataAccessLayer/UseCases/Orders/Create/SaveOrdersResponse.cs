using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Orders.Create
{ 
    public class SaveOrdersResponse
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }

        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public int ProductQuantity { get; set; }
        public int? Discount { get; set; }
        public int? TotalPrice { get; set; }
        public int? TotalAmount { get; set; }

        public string? OrderStatus { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? ProductPrice { get; set; }

        public int TotalProductPrice { get; set; }
        public bool IsActive { get; set; }

    }
}
