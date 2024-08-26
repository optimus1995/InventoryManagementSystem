using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApplicationCore.UseCases.Category.ReadCategory
{ 
    public class ReadCategoryResponse
    {
        public IEnumerable<ApplicationCore.DapperEntity.Category> Categories { get; set; }
        public int Id { get; set; }     
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
