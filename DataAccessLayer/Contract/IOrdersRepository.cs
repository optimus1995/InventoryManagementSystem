using ApplicationCore.DapperEntity;
using ApplicationCore.UseCases.Orders.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract
{
    public interface IOrdersRepository
    {

        public  Task<IEnumerable<OrderItems>> Result();
        public Task<IEnumerable<OrderItems>> ResultByOrderId(int Orderid);

        public Task<IEnumerable<BarChartResponse>> BarChartOrderDetail();

        public Task< SaveOrdersResponse> CreateOrders(ApplicationCore.UseCases.Orders.Create.SaveOrdersRequest orders);
        public Task<(int CustomersCount, int OrdersCount)> GetCount();

    }
}
