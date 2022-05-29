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
        public JwtTokenHelper()
        {

        }

        public static string IssueJWT(ModelToken modelToken, TimeSpan expiresSliding, TimeSpan expiresAbsoulte)
        {
            DateTime UTC = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,modelToken.Sub),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,UTC.ToString(),ClaimValueTypes.Integer64)
            };
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "ColudWhales",      //jwt签发者, 非必须
                audience: modelToken.UName, //jwt接收方, 非必须
                claims: claims,             //声明集合
                expires: UTC.AddHours(12),  //token 声明周期
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ColudWhales Secret Key")),
                    SecurityAlgorithms.HmacSha256));    //使用私密钥匙进行签名加密
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            MemoryCaching.Set(encodedJwt, modelToken, expiresSliding, expiresAbsoulte);
            return encodedJwt;
        }
    }
}
