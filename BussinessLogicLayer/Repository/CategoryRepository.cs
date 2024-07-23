using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Dapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.Data;
using ApplicationCore.DapperEntity;
using ApplicationCore.Contract;
using ApplicationCore.Context;

namespace Infrastructure.Repository
{ 

    public class CategoryRepository : ICategoryRepository
    {
        public readonly DapperContext _Context;
        public CategoryRepository(DapperContext context)
        {

            _Context = context;
        }


        public async Task<Category> Create(Category category)
        {

            var query = "INSERT INTO Category (Name, IsActive) " +
                        "VALUES (@Name, @IsActive);";

            var parameters = new DynamicParameters();
            parameters.Add("@Name", category.Name, DbType.String); ;
            parameters.Add("@IsActive", true, DbType.Int32);

            using (var connection = _Context.CreateConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    await connection.ExecuteAsync(query, parameters);

                    var created = new Category
                    {

                        // Use the newly generated Id
                        Id = category.Id,
                        Name = category.Name,

                        IsActive = category.IsActive
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


        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                var q = " SELECT c.ID,  c.Name, COUNT(p.Id) AS Count FROM Category c  JOIN Products p ON c.ID = p.CategoryId GROUP BY   c.ID, c.Name";
                using (var connection = _Context.CreateConnection())
                {
                    var data = await connection.QueryAsync<Category>(q);
                    var query = data.ToList();
                    // logger.LogInformation("Fetched categories with product counts: {@query}", query);

                    return query;


                }
            }
            catch (Exception ex)
            {
                // Handle the exception as necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

        }


        public async Task<Category> GetrecordforUpdate(int id)
        {
            var query = "SELECT * FROM Category WHERE Id = @Id";

            using (var connection = _Context.CreateConnection())
            {
                var category = await connection.QuerySingleOrDefaultAsync<Category>(query, new { Id = id });
                return category;
            }
        }

        public async Task<Category> Update(Category category)
        {
            int id = category.Id;
            var query = "Update  Category  set Name=@Name where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", category.Id, DbType.Int32);
            parameters.Add("@Name", category.Name, DbType.String); ;

            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
            return category;

        }

        public async Task DeleteRecord(int id)
        {
            var deletquery = "delete from Category where ID=@Id";
            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(deletquery, new { id });
            }
        }




    }
}
