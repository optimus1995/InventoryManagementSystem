using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contract
{
    public interface IJwtTokenServices
    {

        public bool  ValidateToken(string token);

        public string GenerateToken(string userId, string Email);
    }
}
