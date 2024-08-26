using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Products.Read
{
    public class ReadProductsResponse
    {

        public List<ApplicationCore.DapperEntity.Products> Products { get; set; }
        public List<ApplicationCore.DapperEntity.Category> Categories { get; set; }

    }
}
