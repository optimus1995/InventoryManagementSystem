﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Products.SaveImages
{ 
    public class SaveImagesProductResponse
    {
       public int Id {  get; set; }

        public string ProductId { get; set; }

        public string ImageName { get; set; }

        public string ImagesPath { get; set; }

        public string? ProductName { get; set; }

        public int ImageSize { get; set; }

        public string ImageType { get; set; }




    }
}
