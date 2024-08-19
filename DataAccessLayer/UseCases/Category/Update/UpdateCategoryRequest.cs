using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationCore.UseCases.Category.Update
{
    public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}
