using ApplicationCore.DapperEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contract;
using ApplicationCore.Context;
using Microsoft.Extensions.Logging;
using ApplicationCore;
using Dapper;
using System.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace Infrastructure.Repository
{
    public class CustomersRepository : ICustomersRepository
    {

        private readonly DapperContext _Context;
        public CustomersRepository(DapperContext context)
        {
            //       _productRepository = productsRepository;
            _Context = context;

        }
        public async Task<Customers> CreateRecord(Customers customers)
        {
            var query = "INSERT INTO Customers (Name, Email, BillingAddress, ShippingAddress, CreatedBy ,CreatedAt) " +
                        "VALUES (@Name, @Email, @BillingAddress, @ShippingAddress, @CreatedBy,@CreatedAt);";


            var parameters = new DynamicParameters();
            //       parameters.Add("@Id", product.Id, DbType.Int32);
            parameters.Add("@Name", customers.Name, DbType.String); ;
            parameters.Add("@Email", customers.Email, DbType.String);
            parameters.Add("@BillingAddress", customers.BillingAddress, DbType.String); // Assuming SKU is a string
            parameters.Add("@ShippingAddress", customers.ShippingAddress, DbType.String); // Assuming Price is a decimal
            parameters.Add("@CreatedAt", customers.CreatedAt, DbType.DateTime);
            parameters.Add("@CreatedBy", customers.CreatedBy, DbType.String);

            using (var connection = _Context.CreateConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    await connection.ExecuteAsync(query, parameters);

                    var created = new Customers
                    {

                        Id = customers.Id,
                        Name = customers.Name,
                        Email = customers.Email,
                        BillingAddress = customers.BillingAddress,
                        ShippingAddress = customers.ShippingAddress,
                        CreatedAt = customers.CreatedAt,
                        CreatedBy = customers.CreatedBy,
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
    
       
        public async Task<IEnumerable<Customers>> GetAll(string userid)
        {
            //var i = 7;

            var q = @"Select 
                    p.Id ,
                    p.Name ,
                    p.Email,
                    p.BillingAddress,
                    p.ShippingAddress,
                    p.CreatedAt,
                    asp.Id,
                    asp.UserName
                    from Customers p
                    join AspNetUsers asp on asp.Id=p.CreatedBy

            where p.CreatedBy =@userid";
            //where p.UserId=  '6370e636-8165-4711-b391-da621035d6a4'     ";
            using (var connection = _Context.CreateConnection())
            {
                var query = q;
                var customers = await connection.QueryAsync<Customers,  AspNetUsers, Customers>(
                    q,
                    (customers,  aspnetusers) =>
                    {
                        customers.AspNetUsers = aspnetusers;

                        return customers;
                    },
                      new { userid }

                );
                return customers.ToList();
            }
        }
        public async Task<Customers> GetrecordforUpdate(int id)
        {
            var Id = id;
            var query = "SELECT * FROM Customers WHERE Id = @Id";

            using (var connection = _Context.CreateConnection())
            {
                var customer = await connection.QuerySingleOrDefaultAsync<Customers>(query, new { Id = id });
                return customer;
            }
        }

        public async Task<Customers> Update(Customers customers)
        {
            int id = customers.Id;
            var query = "Update  Customers  set Name=@Name, Email=@Email,BillingAddress= @BillingAddress, ShippingAddress=@ShippingAddress where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", customers.Id, DbType.Int32);
            parameters.Add("@Name", customers.Name, DbType.String); ;
            parameters.Add("@Email", customers.Email, DbType.String); ;
            parameters.Add("@BillingAddress", customers.BillingAddress, DbType.String); ;
            parameters.Add("@ShippingAddress", customers.ShippingAddress, DbType.String); ;
            parameters.Add("@UpdatedAt", customers.UpdatedAt, DbType.String); ;
            parameters.Add("@UpdatedBy", customers.UpdatedBy, DbType.String); ;

            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
            return customers;
        }

        public async Task DeleteRecord(int id)
        {
            var deletequery = "delete from Customers where Id=@id";
            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(deletequery, new { id });
            }
        }


    }
}
