using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using MediatR;

namespace ApplicationCore.UseCases.Products.Update
{
    public class GetProductsRequest : IRequest<GetProductsResponse>
    {

        public int Id { get; set; }
       
    }
}
