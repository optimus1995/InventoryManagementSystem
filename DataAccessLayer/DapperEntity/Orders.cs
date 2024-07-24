using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{
    public class Orders
    { 
        public int Id { get; set; } 
        public int CustomerId { get; set; }
        public int  TotalPrice { get; set; }
        
        public int Discount { get; set; }
        public int TotalAmount { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
         public string? OrderStatus { get; set; }
        public bool? IsActive { get; set; } 
        public List<OrderDetails> ListDetails { get; set; } = new List<OrderDetails>();

        public string? isActive { get; set; }
    }
}
