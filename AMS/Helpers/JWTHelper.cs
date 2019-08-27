using AMS.Common.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Helpers
{
    public class JWTHelper
    {
        public static string BuildJwtToken(Claim[] claims, AudienceConfiguration audienceConfiguration)
        {
            var keyByteArray = Encoding.ASCII.GetBytes(audienceConfiguration.Secret);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.Now;
            // 实例化JwtSecurityToken
            var jwt = new JwtSecurityToken(
                issuer: audienceConfiguration.Issuer,
                audience: audienceConfiguration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(audienceConfiguration.Expiration),
                signingCredentials: signingCredentials
            );
            // 生成 Token
            var responseJson = new JwtSecurityTokenHandler().WriteToken(jwt);
            return responseJson;
        }
    }
}
