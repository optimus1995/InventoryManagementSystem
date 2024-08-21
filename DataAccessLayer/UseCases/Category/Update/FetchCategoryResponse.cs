using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Category.Update
{ 
    public class FetchCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Count { get; set; }
        public int IsActive { get; set; }

    }
}
