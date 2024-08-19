using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Category.Create
{ 
    public class CreateCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}
