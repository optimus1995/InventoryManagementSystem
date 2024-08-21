using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;


namespace ApplicationCore.UseCases.Orders.Create
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
