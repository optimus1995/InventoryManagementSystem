using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Products.DisplayImages
{
    public class DisplayImagesResponse
    {

       public  ApplicationCore.DapperEntity.Products products { get; set; }

        public  List<ApplicationCore.DapperEntity.ProductImages > productsImage { get; set; }


    }
}
