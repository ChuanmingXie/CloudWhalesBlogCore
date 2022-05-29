/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Common
*项目描述:
*类 名 称:JwtTokenHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 18:40:36
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.IdentityModel.Tokens;
using SwaggerWithMiniProfiler.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Common
{
    public class JwtTokenHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(ModelToken tokenModel)
        {
            string iss = "CloudWhales";
            string aud = "Blog";
            string secret = "ColudWhales Secret Key";
            //var claims = new Claim[] //old

            /* 特别重要：
              1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法
              2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
            */
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss,iss),
                    new Claim(JwtRegisteredClaimNames.Aud,aud),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Role,"Client"),
                    new Claim(ClaimTypes.Role,"System")
               };
            claims.Add(new Claim("Admin", "Test"));
            claims.Add(new Claim("Admin", "Test1"));
            claims.Add(new Claim("Admin", "Test2"));
            claims.Add(new Claim("Admin", "Test3"));

            // 可以将一个用户的多个角色全部赋予；
            //claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static ModelToken SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new ModelToken
            {
                Uid = long.Parse(jwtToken.Id),
                Role = role != null ? role.ToString() : "",
            };
            return tm;
        }
    }
}
