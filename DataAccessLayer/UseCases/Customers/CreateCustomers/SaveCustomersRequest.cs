using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
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
