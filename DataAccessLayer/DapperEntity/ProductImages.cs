using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DapperEntity
{
    public class ProductImages
    {
        int Id { get; set; }
     //    public string Name { get; set; }
        public string ImageName { get; set; }
        public string? ProductName { get; set; }

        public string ImagesPath { get; set; }

        public string ProductId { get; set; }

        public int ImageSize { get; set; }   

        public string ImageType { get; set; }
    }
}
