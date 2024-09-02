using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;


namespace ApplicationCore.UseCases.Products.DisplayImages
{
    public class DisplayImagesRequest : IRequest<DisplayImagesResponse>
    {
        public int Id { get; set; }
       }
}
