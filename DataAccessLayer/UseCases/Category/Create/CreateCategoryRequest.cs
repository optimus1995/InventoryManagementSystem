using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationCore.UseCases.Category.Create
{
    public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
    {
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}
