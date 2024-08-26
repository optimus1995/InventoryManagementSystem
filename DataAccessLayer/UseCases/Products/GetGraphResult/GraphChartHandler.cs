using ApplicationCore.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.DapperEntity;
using System.Linq;

namespace ApplicationCore.UseCases.Products.GetGraphChart
{
    public class GraphChartHandler : IRequestHandler<GraphChartRequest, GraphChartResponse>
    {
        private readonly IProductsRepository _products;

        public GraphChartHandler(IProductsRepository productsRepository)
        {

            _products = productsRepository;
        }

        public async Task<GraphChartResponse> Handle(GraphChartRequest request, CancellationToken cancellationToken)
        {
            var record = await _products.GetCountforChart ();
            var rec = record.ToList();
            

            // Create and return the GraphChartResponse
            return new GraphChartResponse
            {
                productCategoryGraphs=rec
              
            };
        }
    }


}
