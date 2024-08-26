using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Orders.SpecificOrder
{
    public class SpecificOrdersRequest : IRequest<SpecificOrdersResponse>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SKU { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public string? UserId { get; set; }
        public ApplicationCore.DapperEntity.Category? Category { get; set; }
        public ApplicationCore.DapperEntity.AspNetUsers? AspNetUsers { get; set; }
    }
}
