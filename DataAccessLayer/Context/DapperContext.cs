using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ApplicationCore.Context
{
    public class DapperContext
    {

        public readonly IConfiguration _Configuration;
        public readonly string _ConnectionString;

        public DapperContext(IConfiguration Configuration)
        {
            _Configuration = Configuration;
            _ConnectionString = _Configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_ConnectionString);





    }
}
