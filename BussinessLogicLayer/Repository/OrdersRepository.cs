using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Context;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Infrastructure.Repository
{

    public class OrdersRepository : IOrdersRepository
    {

        private DapperContext _Context;

        public OrdersRepository(DapperContext dapperContext) {
            _Context = dapperContext;

        }
        //public async Task<IEnumerable<OrderItems>> Result()
        //{
        //    var q = @"
        //        SELECT 
        //        ord.Id ,
        //        ord.TotalPrice,
        //        ord.Discount,
        //        ord.TotalAmount,
        //        ord.CreatedBy,
        //        ord.OrderStatus,
        //        ord.CustomerId,
        //        os.ProductId,

        //        os.OrderId,
        //        os.Quantity,
        //        s.Id,
        //        s.Name,
        //        cus.Id,
        //        cus.Name
        //    FROM OrderItems os
        //     JOIN Orders ord ON ord.Id = os.OrderId
        //    left JOIN Products s ON s.Id = os.ProductId
        //    left join Customers  cus on cus.Id=ord.CustomerId ";

        //    using (var connection = _Context.CreateConnection())
        //    {                                                                                          
        //        var query = q;
        //        var ordersrecord = await connection.QueryAsync<OrderItems, Orders, Products,Customers, OrderItems>(
        //            query,
        //            (orderItems, orders, products,customers) =>
        //            {
        //                orderItems.Orders = orders;

        //                orderItems.Customers= customers;

        //                orderItems.Products = products;
        //                return orderItems;
        //            }
        //        );
        //        return ordersrecord.ToList();
        //    }
        //}
        //        public async Task<IEnumerable<OrderItems>> Result()
        //        {

        //            var query = @"
        //                SELECT 
        //                ord.Id ,
        //                ord.TotalPrice,
        //                ord.Discount,
        //                ord.TotalAmount,
        //                ord.CreatedBy,
        //                ord.OrderStatus,
        //                ord.CustomerId,
        //                orditem.ProductId,

        //                orditem.OrderId,
        //                orditem.Quantity,
        //                s.Id,
        //                s.Name
        //             FROM OrderItems orditem
        //             JOIN Orders ord ON ord.Id = orditem.OrderId
        //            left JOIN Products s ON s.Id = orditem.ProductId

        //";

        //            using (var connection = _Context.CreateConnection())
        //            {
        //                var ordersRecord = await connection.QueryAsync<OrderItems, Orders, OrderItems>(
        //                    query,
        //                    (orderItems, orders) =>
        //                    {
        //                        orderItems.Orders = orders;
        //                        //orderItems.Customers = customers;
        //                        return orderItems;
        //                    }
        //                    //,
        //                    //splitOn: "TotalPrice,Quantity,Name"
        //                );
        //                return ordersRecord.ToList();
        //            }
        //        }
        //    public async Task<IEnumerable<OrderItems>> Result()
        //    {
        //        var query = @"
        //SELECT 
        //    orditem.ProductId,
        //    orditem.OrderId,
        //    orditem.Quantity,
        //    ord.Id as OrderId,
        //    ord.TotalPrice,
        //    ord.Discount,
        //    ord.TotalAmount,
        //    ord.CreatedBy,
        //    ord.OrderStatus,
        //    ord.CustomerId,
        //    s.Id as ProductId,
        //    s.Name as ProductName,
        //    cus.Id as CustomerId,
        //    cus.Name as CustomerName
        //FROM OrderItems orditem
        //JOIN Orders ord ON orditem.OrderId = ord.Id 
        //JOIN Products s ON  orditem.ProductId=s.Id 
        // JOIN Customers cus ON ord.CustomerId= cus.Id";

        //        using (var connection = _Context.CreateConnection())
        //        {
        //            var ordersRecord = await connection.QueryFirstAsync(
        //                query);
        //                //(orderItems, orders, products, customers) =>
        //                //{
        //                //    orderItems.Orders = orders;
        //                //    orderItems.Products = products;
        //                //    orderItems.Customers = customers;
        //                //    return orderItems;
        //                //}
        //                //splitOn: "OrderId,ProductId,CustomerId"


        //            return ordersRecord.ToList();
        //        }
        //    }
        public async Task<IEnumerable<OrderItems>> Result()
        {
            var query = @"
SELECT  distinct
    orditem.ProductId,
    orditem.OrderId,
    orditem.Quantity,
    ord.Id as OrderId,
    ord.TotalPrice,
    ord.Discount,
    ord.TotalAmount,
    ord.CreatedBy,
    ord.OrderStatus,
    ord.CustomerId,
    prod.Id as ProductId,
    prod.Name ,
    cus.Id as CustomerId,
    cus.Name,
    cus.ShippingAddress,
    cus.BillingAddress
FROM OrderItems orditem
JOIN Orders ord ON orditem.OrderId = ord.Id 
JOIN Products prod ON orditem.ProductId = prod.Id 
JOIN Customers cus ON ord.CustomerId = cus.Id
where ord.IsActive =1";

            using (var connection = _Context.CreateConnection())
            {
                var ordersRecord = await connection.QueryAsync<OrderItems, Orders, Products, Customers, OrderItems>(
                    query,
                    (orderItems, orders, products, customers) =>
                    {
                        orderItems.Orders = orders;
                        orderItems.Products = products;
                        orderItems.Customers = customers;
                        return orderItems;
                    },
                    splitOn: "OrderId,ProductId,CustomerId"
                );

                return ordersRecord.ToList();
            }
        }
        public async Task<OrderDetails> CreateOrders(Orders orders)
        {
            var orderquery = "insert into Orders (CustomerId,TotalPrice,Discount,CreatedAt,CreatedBy,OrderStatus,TotalAmount,IsActive)" +
                        "VALUES (@CustomerId, @TotalPrice, @Discount, @CreatedAt, @CreatedBy,@OrderStatus,@TotalAmount, @IsActive) ;" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";


            var parameters = new DynamicParameters();
            parameters.Add("@CustomerId", orders.CustomerId, DbType.Int32);
            parameters.Add("@TotalPrice", orders.TotalPrice, DbType.Int32);
            parameters.Add("@Discount", orders.Discount, DbType.Int32);
            parameters.Add("@OrderStatus", orders.OrderStatus, DbType.String); // Assuming Price is a decimal
            parameters.Add("@CreatedAt", orders.CreatedAt, DbType.DateTime);
            parameters.Add("@CreatedBy", orders.CreatedBy, DbType.String);
            parameters.Add("@TotalAmount", orders.TotalAmount, DbType.Int32); // Assuming SKU is a string
            parameters.Add("@IsActive", orders.IsActive, DbType.Boolean);
            using (var connection = _Context.CreateConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    var s = await connection.ExecuteScalarAsync<int>(orderquery, parameters);
                    int ordid = Convert.ToInt32(s);

                    foreach (var detail in orders.ListDetails)
                    {

                        var orderId = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price, TotalPrice) 
                                          VALUES (@OrderId, @ProductId, @Quantity, @Price, @TotalPrice);";

                        var itemParameters = new DynamicParameters();
                        itemParameters.Add("@OrderId", s, DbType.Int32);
                        itemParameters.Add("@ProductId", detail.ProductId, DbType.Int32);
                        itemParameters.Add("@Quantity", detail.ProductQuantity, DbType.Int32);
                        itemParameters.Add("@Price", detail.ProductPrice, DbType.Int32);
                        itemParameters.Add("@TotalPrice", detail.TotalPrice, DbType.Int32);
                        await connection.ExecuteAsync(orderId, itemParameters);

                    }
                    //var orderId = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, Price, TotalPrice) 
                    //                  VALUES (@OrderId, @ProductId, @Quantity, @Price, @TotalPrice);";

                    //var itemParameters = new DynamicParameters();
                    //itemParameters.Add("@OrderId", s, DbType.Int32);
                    //itemParameters.Add("@ProductId", orders.ProductId, DbType.Int32);
                    //itemParameters.Add("@Quantity", orders.ProductQuantity, DbType.Int32);
                    //itemParameters.Add("@Price", orders.ProductPrice, DbType.Int32);
                    //itemParameters.Add("@TotalPrice", orders.TotalProductPrice, DbType.Int32);


                    //await connection.ExecuteAsync(orderId, itemParameters);

                    var created = new OrderDetails
                    {
                        CustomerId = orders.CustomerId,
                        TotalPrice = orders.TotalPrice,
                        Discount = orders.Discount,
                        OrderStatus = orders.OrderStatus,
                        CreatedAt = orders.CreatedAt,
                        CreatedBy = orders.CreatedBy,
                        TotalAmount = orders.TotalAmount,
                        OrderId = ordid,
                        //ProductId=orders.ProductId
                        //  TotalProductPrice = orders.TotalProductPrice,
                        //    ProductQuantity = orders.ProductQuantity,
                        //    ProductPrice = orders.ProductPrice,




                    };

                    return created;
                }
                catch (Exception ex)
                {
                    // Handle the exception as necessary
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }


        public async Task<IEnumerable<OrderItems>> ResultByOrderId(int id)
        {
            try {
                var query = @"
SELECT 
    orditem.Id ,
    orditem.ProductId,
    orditem.TotalPrice,
    orditem.OrderId,
    orditem.Quantity,
    orditem.Price,
    ord.Id as OrderId,
    ord.TotalPrice,
    ord.Discount,
    ord.TotalAmount,
    ord.CreatedBy,
    ord.CreatedAt,
    ord.OrderStatus,
    ord.CustomerId,
    prod.Id as ProductId,
    prod.Name ,
    
    cus.Id as CustomerId,
    cus.Name,
cus.ShippingAddress,
    cus.BillingAddress
FROM OrderItems orditem
JOIN Orders ord ON orditem.OrderId = ord.Id 
JOIN Products prod ON orditem.ProductId = prod.Id 
JOIN Customers cus ON ord.CustomerId = cus.Id
where ord.IsActive =1 and ord.Id=@id";

                using (var connection = _Context.CreateConnection())
                {
                    var ordersRecord = await connection.QueryAsync<OrderItems, Orders, Products, Customers, OrderItems>(
                        query,
                        (orderItems, orders, products, customers) =>
                        {
                            orderItems.Orders = orders;
                            orderItems.Products = products;
                            orderItems.Customers = customers;
                            return orderItems;
                        }, new { id },
                        splitOn: "OrderId,ProductId,CustomerId"
                    );

                    return ordersRecord.ToList();
                }
            }
        
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                throw;
            }
             
        }



        public async Task<( int CustomersCount, int OrdersCount)> GetCount()
        {
            // Correct SQL query to get counts
            var query = @"
        SELECT 
            (SELECT COUNT(*) FROM Customers) AS CustomersCount,
            (SELECT COUNT(*) FROM Orders) AS OrdersCount"

            ;


            using (var connection = _Context.CreateConnection())
            {
                // Execute the query and get the result as a single row
                var result = await connection.QuerySingleAsync<( int CustomersCount, int OrdersCount)>(query);
                return result;
            }
        }

        public async Task<IEnumerable<BarChartOrder>> BarChartOrderDetail()
        {
            var orderquery = "RecordforBarChart"; 
            using (var connection = _Context.CreateConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    var result = await connection.QueryAsync<BarChartOrder>(
                        orderquery,
                        commandType: CommandType.StoredProcedure
                    );
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }



    }
}
