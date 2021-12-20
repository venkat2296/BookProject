using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookMVC.Models
{
    public class TokenService:ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;
        public string BuildToken(string key,string issuer,LoginViewModel model)
        {
            var Claims = new[] {
                new Claim(ClaimTypes.Name,model.UserName),
                new Claim(ClaimTypes.Email,model.UserName),
                new Claim(ClaimTypes.NameIdentifier,new Guid().ToString())
            };


            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var tokendescriptor = new JwtSecurityToken(issuer, issuer, Claims, expires:DateTime.Now.AddHours(5), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokendescriptor);

        }
    }

    public interface ITokenService
    {
        string BuildToken(string key, string issuer, LoginViewModel user);
        //bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
