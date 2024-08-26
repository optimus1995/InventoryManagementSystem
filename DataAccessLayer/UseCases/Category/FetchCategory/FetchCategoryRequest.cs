using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicationCore.UseCases.Category.FetchCategory
{
    public class FetchCategoryRequest : IRequest<FetchCategoryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Count { get; set; }
        public int IsActive { get; set; }
    }
}
