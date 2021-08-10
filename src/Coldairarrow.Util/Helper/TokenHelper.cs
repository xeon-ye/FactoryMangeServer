using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class TokenHelper
    {
        public static  JwtSecurityToken GetToen(string userId, string Secret,int AccessExpireHours)
        {
            

            var claims = new[]
            {
                new Claim("userId",userId)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                string.Empty,
                string.Empty,
                claims,
                expires: DateTime.Now.AddHours(AccessExpireHours),
                signingCredentials: credentials);

            return jwtToken;
        }
    }
}
