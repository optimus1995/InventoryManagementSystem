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
using System.Data.SqlTypes;
using ApplicationCore.UseCases.Employee.UpdateRoleEmployee;


namespace Infrastructure.Repository
{

    public class EmployeeRepository : IEmployeesRepository
    {

        private DapperContext _Context;

        public EmployeeRepository(DapperContext dapperContext)
        {
            _Context = dapperContext;

        }
        public async Task<IEnumerable<AspNetUsers>> GetAll()
        {
            var query = @"
    SELECT 
        asp.Id,
        asp.UserName,
        asp.NormalizedEmail,
        asp.LockOutEnd,
        aspnetuserrole.RoleId,
        asproles.Id,
        asproles.NormalizedName
    FROM AspNetUsers asp
    JOIN AspNetUserRoles aspnetuserrole ON asp.Id = aspnetuserrole.UserId 
    JOIN AspNetRoles asproles ON asproles.Id = aspnetuserrole.RoleId

    ";

            using (var connection = _Context.CreateConnection())
            {
                var records = await connection.QueryAsync<AspNetUsers, AspNetRoles, AspNetUsers>(
                    query,
                    (asp, asproles) =>
                    {
                        asp.AspNetRoles = asproles; // Assign AspNetRoles to AspNetUsers
                        return asp;
                    },
                    splitOn: "RoleId" // Split at RoleId to correctly map AspNetRoles
                );

                return records.ToList();
            }
        }




        public async Task<IEnumerable<AspNetRoles>> GetAllRoles()
        {
            try
            {
                var query = "SELECT aspr.ID, aspr.Name FROM AspNetRoles aspr";
                using (var connection = _Context.CreateConnection())
                {
                    // Ensure that you map the result to AspNetRoles, not Category
                    var data = await connection.QueryAsync<AspNetRoles>(query);
                    return data; // Convert to List if needed, or just return data as IEnumerable
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<AspNetUsers> GetRoleData(string id)
        {
            try
            {
                var query = @"
SELECT 
    asp.Id, 
    asp.UserName, 
    aspuserroles.RoleId, 
    asproles.Id as RoleId, 
    asproles.NormalizedName,
    asp.LockOutEnd
FROM 
    AspNetUsers asp
JOIN 
    AspNetUserRoles aspuserroles ON aspuserroles.UserId = asp.Id
JOIN 
    AspNetRoles asproles ON asproles.Id = aspuserroles.RoleId
WHERE 
    asp.Id = @id
        ";

                using (var connection = _Context.CreateConnection())
                {
                    var records = await connection.QueryAsync<AspNetUsers, AspNetUserRoles, AspNetRoles, AspNetUsers>(
                        query,
                        (aspnet, aspnetuserroles, aspnetroles) =>
                        {
                            aspnet.AspNetUserRoles = aspnetuserroles;
                            aspnet.AspNetRoles = aspnetroles;
                            return aspnet;
                        },
                        new { id },
                        splitOn: "RoleId"
                    );

                    return records.FirstOrDefault();  // Or .SingleOrDefault() if you expect exactly one result
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                throw;
            }
        }

        public async Task<UpdateRoleRequest> UpdateRoles(UpdateRoleRequest asproles)
        {
            var query = "UPDATE AspNetUserRoles SET RoleId = @RoleId WHERE UserId = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", asproles.RoleId, DbType.String);
            parameters.Add("@UserId", asproles.UserId, DbType.String);

            using (var connection = _Context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

            return asproles;
        }

    }
}
