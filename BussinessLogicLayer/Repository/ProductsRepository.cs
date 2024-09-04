using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Dapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApplicationCore.Contract;
using ApplicationCore.DapperEntity;
using ApplicationCore.Context;

namespace Infrastructure.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        //     public readonly IProductsRepository _productRepository;
        private readonly ILogger<CategoryRepository> logger;

        public readonly DapperContext _Context;
        public ProductsRepository(DapperContext context)
        {
            //       _productRepository = productsRepository;
            _Context = context;

        }


        //public async Task<Products> CreateProducts(Products product)
        //{
        //    var query = "INSERT INTO Products (Id,Name, Description, SKU,Price,Quantity) " +
        //                "VALUES (@Id,@Name, @Description, @SKU);" +
        //                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

        //    var parameters = new DynamicParameters();
        //    parameters.Add("@Name", product.Name, DbType.String);
        //    parameters.Add("@Description", product.Description, DbType.String);
        //    parameters.Add("@SKU", product.SKU, DbType.Int32);
        //    parameters.Add("@Price", product.Price, DbType.Int32);
        //    parameters.Add("@Quantity", product.quantity, DbType.Int32);
        //    parameters.Add("@Id", product.Id, DbType.Int32);

        //    using (var connection = _Context.CreateConnection())
        //    {
        //        var sqlConnection1 = (Microsoft.Data.SqlClient.SqlConnection)connection;

        //        // Ensure the connection is open
        //        if (sqlConnection1.State != System.Data.ConnectionState.Open)
        //        {
        //            sqlConnection1.Open();
        //        }

        //        // Now try to access the ServerVersion property
        //        string serverVersion = sqlConnection1.ServerVersion;
        //        Console.WriteLine($"SQL Server Version: {serverVersion}");
        //        Console.WriteLine("212");
        //        var id = await connection.QuerySingleAsync<int>(query, parameters);
        //        var created = new Products
        //        {
        //            Id = product.Id,
        //            Name = product.Name,
        //            Description = product.Description,
        //            SKU = product.SKU,
        //            Price = product.Price,
        //            quantity = product.quantity
        //        };
        //        return created;
        //    }

        //}


        public async Task<Products> CreateProducts(Products product)
        {
            var query = "INSERT INTO Products (Name, Description, SKU, Price, Quantity ,CreatedAt,IsActive,CategoryID,UserId) " +
                        "VALUES (@Name, @Description, @SKU, @Price, @Quantity,@CreatedAt, @IsActive,@CategoryID, @UserId );";


            var parameters = new DynamicParameters();
            //       parameters.Add("@Id", product.Id, DbType.Int32);
            parameters.Add("@Name", product.Name, DbType.String); ;
            parameters.Add("@Description", product.Description, DbType.String);
            parameters.Add("@SKU", product.SKU, DbType.Int32); // Assuming SKU is a string
            parameters.Add("@Price", product.Price, DbType.Decimal); // Assuming Price is a decimal
            parameters.Add("@Quantity", product.quantity, DbType.Int32);
            parameters.Add("@CreatedAt", product.CreatedAt, DbType.DateTime);
            parameters.Add("@IsActive", true, DbType.Int32);
            parameters.Add("@CategoryID", product.CategoryID, DbType.Int32);
            parameters.Add("@UserId", product.UserId, DbType.String);

            using (var connection = _Context.CreateConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    await connection.ExecuteAsync(query, parameters);

                    var created = new Products
                    {

                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        SKU = product.SKU,
                        Price = product.Price,
                        quantity = product.quantity,
                        CreatedAt = product.CreatedAt,
                        IsActive = product.IsActive,
                        CategoryID = product.CategoryID,
                        UserId = product.UserId
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

        public async Task<IEnumerable<Products>> GetAll(string userid)
        {
            //var i = 7;

            var q = @"Select 
                    p.Id ,
                    p.Name ,
                    p.Price,
                    p.Quantity,
                    p.SKU,
                    p.Description,
                    p.CreatedAt,
                    p.CategoryID,
                    s.Id,
                    s.Name,
                    asp.Id,
                    asp.UserName
                    from Products p
                    join Category s  on s.Id=p.CategoryID  
                    join AspNetUsers asp on asp.Id=p.UserId

            where p.UserId =@userid";
            //where p.UserId=  '6370e636-8165-4711-b391-da621035d6a4'     ";
            using (var connection = _Context.CreateConnection())
            {
                var query = q;
                var products = await connection.QueryAsync<Products, Category, AspNetUsers, Products>(
                    q,
                    (product, category, aspnetusers) =>
                    {
                        product.Category = category;
                        product.AspNetUsers = aspnetusers;

                        return product;
                    },
                      new { userid }

                );
                return products.ToList();
            }
        }
        public async Task<Products> GetrecordforUpdate(int id)
        {
            var productid = id;
            var q = @"SELECT 
                 p.Id,
                 p.Name,
                 p.Price,
                 p.Quantity,
                 p.SKU,
                 p.Description,
                 p.CreatedAt,
                 p.CategoryID,
                 s.Id AS CategoryId,
                 s.Name AS CategoryName
             FROM Products p
             JOIN Category s ON s.Id = p.CategoryID
             WHERE p.Id = @ProductId";

            using (var connection = _Context.CreateConnection())
            {
                var products = await connection.QueryAsync<Products, Category, Products>(
                    q,
                    (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    new { ProductId = productid },
                    splitOn: "CategoryId"
                );

                return products.FirstOrDefault();
            }
        }

        public async Task<Products> UpdateProducts(Products product)
        {
            int id = product.Id;
            var query = "Update  Products  set Name=@Name, Description=@Description, SKU = @SKu, Price=@Price, quantity=@Quantity , LastUpdatedAt=@LastUpdate, CategoryID=@CategoryID where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", product.Id, DbType.Int32);
            parameters.Add("@Name", product.Name, DbType.String); ;
            parameters.Add("@Description", product.Description, DbType.String);
            parameters.Add("@SKU", product.SKU, DbType.Int32); // Assuming SKU is a string
            parameters.Add("@Price", product.Price, DbType.Decimal); // Assuming Price is a decimal
            parameters.Add("@Quantity", product.quantity, DbType.Int32);
            parameters.Add("@LastUpdate", DateTime.Now, DbType.DateTime);
            parameters.Add("@CategoryID", product.CategoryID, DbType.Int32);

            //parameters.Add("@IsActive", true, DbType.Int32);
            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
            return product;

        }

        public async Task DeleteRecord(int id)
        {
            var deletquery = "delete from Products where ID=@Id";
            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(deletquery, new { id });
            }
        }

        public async Task<IEnumerable<Products>> ShowByCatID(int catid, string userid)
        {
            var q = @"Select 
                    p.Id ,
                    p.Name ,
                    p.Price,
                    p.Quantity,
                    p.SKU,
                    p.Description,
                    p.CreatedAt,
                    p.CategoryID,
                    s.Id,
                    s.Name,
                    asp.Id,
                    asp.UserName
                    from Products p
                    join Category s  on s.Id=p.CategoryID  
                    join AspNetUsers asp on asp.Id=p.UserId

            where p.CategoryID=@catid and p.UserId =@userid";
            //where p.UserId=  '6370e636-8165-4711-b391-da621035d6a4'     ";
            using (var connection = _Context.CreateConnection())
            {
                var query = q;
                var products = await connection.QueryAsync<Products, Category, AspNetUsers, Products>(
                    q,
                    (product, category, aspnetusers) =>
                    {
                        product.Category = category;
                        product.AspNetUsers = aspnetusers;

                        return product;
                    },
                      new { catid, userid }

                );
                return products.ToList();
            }
        }

        public async Task<(int ProductCount, int CategoryCount)> GetCount()
        {
            var query = @"
        SELECT 
            (SELECT COUNT(*) FROM Products) AS ProductCount, 
            (SELECT COUNT(*) FROM Category) AS CategoryCount
 "

            ;


            using (var connection = _Context.CreateConnection())
            {
                // Execute the query and get the result as a single row
                var result = await connection.QuerySingleAsync<(int ProductCount, int CategoryCount)>(query);
                return result;
            }
        }



        public async Task<IEnumerable<ProductCategoryGraph>> GetCountforChart()
        {
            // Correct SQL query to get counts of products per category
            var query = @"
    SELECT 
        c.Name AS CategoryName, 
        COUNT(p.Id) AS ProductCount
    FROM 
        Category c
    LEFT JOIN 
        Products p ON c.Id = p.CategoryId
    GROUP BY 
        c.Name;";

            using (var connection = _Context.CreateConnection())
            {
                // Execute the query and get the results as a list of ProductCategoryGraph
                var result = await connection.QueryAsync<ProductCategoryGraph>(query);
                return result;
            }
        }
        public async Task<ProductImages> SaveImages(ProductImages productimages)
        {
             var query = "INSERT INTO ProductImages (ImagesPath, ProductId,ImageType,ImageName,ImageSize) " +
                        "VALUES (@ImagesPath, @ProductId,@ImageType,@ImageName,@ImageSize);";

            var parameters = new DynamicParameters();
            parameters.Add("@ImagesPath", productimages.ImagesPath, DbType.String);
            parameters.Add("@ProductId", productimages.ProductId, DbType.String);
            parameters.Add("@ImageName", productimages.ImageName, DbType.String);
            parameters.Add("@ImageType",productimages.ImageType, DbType.String);
            parameters.Add("@ImageSize",productimages.ImageSize, DbType.Int32);

            using (var connection = _Context.CreateConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    await connection.ExecuteAsync(query, parameters);

                    var created = new ProductImages
                    {
                        ProductId = productimages.ProductId,
                        ImagesPath = productimages.ImagesPath // Include this for clarity
                    };

                    return created;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        //public async Task<ProductImages> SaveImages(ProductImages productimages)
        //{
        //    var query = "INSERT INTO ProductImages ( ImagesPath, ProductId) " +
        //                "VALUES ( @ImagePath, @ProductId);";


        //    var parameters = new DynamicParameters();
        //    //       parameters.Add("@Id", product.Id, DbType.Int32);
        //    parameters.Add("@ImagePath", productimages.ImagePath, DbType.String);
        //    parameters.Add("@ProductId", productimages.ProductId, DbType.String);

        //    using (var connection = _Context.CreateConnection())
        //    {
        //        try
        //        {
        //            if (connection.State != ConnectionState.Open)
        //            {
        //                connection.Open();
        //            }
        //            await connection.ExecuteAsync(query, parameters);

        //            var created = new ProductImages
        //            {
        //                ProductId = productimages.ProductId

        //            };

        //            return created;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle the exception as necessary
        //            Console.WriteLine($"An error occurred: {ex.Message}");
        //            throw;
        //        }
        //    }
        //}   


        //public async Task<IEnumerable<ProductImages>> DisplayImages(int productId)
        //{
        //    var q = @"Select 
        //            p.Id ,
        //            p.Name ,
        //            from Products p
        //            join ProductImages prodimgs on prodimgs.ProductId=p.Id

        //    where p.Id =@productId";
        //    //where p.UserId=  '6370e636-8165-4711-b391-da621035d6a4'     ";
        //    using (var connection = _Context.CreateConnection())
        //    {
        //        var query = q;
        //        var products = await connection.QueryAsync<Products,  ProductImages, Products>(
        //            q,
        //            (product,productimages ) =>
        //            {
        //                product.ProductImages = productimages;

        //                return product;
        //            },
        //              new { productId }

        //        );
        //        var products = new Prodcut
        //        return products.ToList();
        //    }
        //}
        public async Task<IEnumerable<ProductImages>> DisplayImages(int productId)
        {
            var query = @"
        SELECT 
           
            p.Name as ProductName,
            
            prodimgs.Id,
            prodimgs.ImagesPath,
            prodimgs.ImageName,
            prodimgs.ImageSize,
            prodimgs.ImageType,
            prodimgs.ProductId
            
        FROM ProductImages prodimgs
        join Products p on p.Id=prodimgs.productId

        WHERE prodimgs.ProductId = @productId";

            using (var connection = _Context.CreateConnection())
            {
                var images = await connection.QueryAsync<ProductImages>(query, new { productId });
                return images;
            }
        }


     //   public async Task<IEnumerable<ProductImages>> DisplayImages(int productId)
     //   {
     //       var q = @"
     //SELECT 
     //       p.Id,
     //       p.Name,
     //       prodimgs.ImagesPath
     //   FROM Products p
     //   JOIN ProductImages prodimgs ON prodimgs.ProductId = p.Id
     //   WHERE p.Id = @productId";
     //       using (var connection = _Context.CreateConnection())
     //       {
     //           var query = q;
     //           var products = await connection.QueryAsync<Products, ProductImages, Products>(
     //               q,
     //               (product, images) =>
     //               {
     //                   product.ProductImages = images;


     //                   return product;
     //               },
     //                 new { productId }

     //           );
     //           return products;
     //       }
     //   }



















    }
}
