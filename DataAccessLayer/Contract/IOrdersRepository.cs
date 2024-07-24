using ApplicationCore.DapperEntity;
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

        public Task<OrderDetails> CreateOrders(Orders orders);

    }
}
