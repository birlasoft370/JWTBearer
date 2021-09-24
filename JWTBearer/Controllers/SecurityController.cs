using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTBearer.Controllers
{
    /// <summary>
    /// Refer https://www.youtube.com/watch?v=bWA-pZJrOFE
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private string GenerateJSONWebToken(string username)// 5. Create Token with some input
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bala@123bala@123"));//2.Key (salt)
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//1.Algorithm

            var claims = new[] {
                new Claim("Issuer", "bala"),
                new Claim("Admin", "true"),
                new Claim(JwtRegisteredClaimNames.UniqueName,username)
            }; //  3. Roles/Claims

            var token = new JwtSecurityToken("bala",
                "bala",
                claims,
                expires: DateTime.Now.AddMinutes(120),//4. Expiry
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

            // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9. -- What kind of Algorithm
            // eyJJc3N1ZXIiOiJiYWxhIiwiQWRtaW4iOiJ0cnVlIiwidW5pcXVlX25hbWUiOiJiYWxhMTIzIiwiZXhwIjoxNjIwMzgyNzgyLCJpc3MiOiJiYWxhIiwiYXVkIjoiYmFsYSJ9. -- has the Claims/Roles/Payload
            // xfkCZvrAZvyMlEHnVTVWk71drj7vQAd77h4nyJpyBy8 -- ALL above two + key


        }

        // GET: api/Security
        [HttpGet]
        public string Get()
        {
            return GenerateJSONWebToken("bala123");
        }

    }
}
