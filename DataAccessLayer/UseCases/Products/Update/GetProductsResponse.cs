using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Products.Update
{ 
    public class GetProductsResponse
    {
        public ApplicationCore.DapperEntity.Products? products { get; set; }
           }
}
