using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationCore.UseCases.Category.DeleteCategory
{
    public class DeleteCategoryRequest : IRequest<DeleteCategoryResponse>
    {
       public int Id { get; set; }  
    }
}
