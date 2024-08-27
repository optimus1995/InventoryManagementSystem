using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;


namespace ApplicationCore.UseCases.Products.ReadProducts
{
    public class ReadProductsRequest : IRequest<ReadProductsResponse>
    {
        public int catid { get; set; }

        public List<ApplicationCore.DapperEntity.Products> Products { get; set; }
        public List<ApplicationCore.DapperEntity.Category> Categories { get; set; }
    }
}
