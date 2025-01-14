using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    using ApplicationCore.Contract;
    using ApplicationCore.DapperEntity;

    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;


    public class JwtServices : IJwtTokenServices
    {
        public readonly IConfiguration _configuration;
 

        public JwtServices( IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //public string  ValidateToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(token);
        //    // Replace with your actual secret key
        //    try
        //    {
        //        var result = tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            //  IssuerSigningKey = "your-very-strong-secret-key"
        //        }, out var validatedToken);

        //        return result;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public bool   ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                // Validation Parameters
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                // Validate token and retrieve claims
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true; // Token is valid
            }
            catch (Exception)
            {
                return false; // Invalid token
            }
        }


        public string GenerateToken(string userId, string Email)
        {
            var expiryminutes = Convert.ToInt32(_configuration["Jwt:ExpirationMinutes"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId),  
        new Claim(ClaimTypes.Email, Email),          
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], 
                audience: _configuration["Jwt:Audience"], 
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryminutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}