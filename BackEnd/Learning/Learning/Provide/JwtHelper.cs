using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Provide
{
    public class JwtHelper
    {
        public static IConfiguration _configs;


        public static string GetToken(string userName)
        {
            var claims = new[]
             {
                   new Claim(ClaimTypes.Name,userName)
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs["JwtOptions:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
            /**
             * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                iss: The issuer of the token，token 是给谁的
                sub: The subject of the token，t oken 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             * */
            var token = new JwtSecurityToken(
                issuer: "jwttest",
                audience: "jwttest",
                claims: claims,
                //notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30),

                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
