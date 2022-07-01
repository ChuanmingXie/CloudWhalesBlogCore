/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.HttpContextUser
*项目描述:
*类 名 称:AspNetUser
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/26 16:34:50
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common.ConvertHelper;
using CloudWhalesBlogCore.Shared.GlobalData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Shared.HttpContextUser
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => GeName();

        private string GeName()
        {
            if (IsAuthenticated() && _accessor.HttpContext.User.Identity.Name.IsNotEmptyOrNull())
            {
                return _accessor.HttpContext.User.Identity.Name;
            }
            else
            {
                if (!string.IsNullOrEmpty(GetToken()))
                {
                    var getNameType = Permissions.IsUseIds4 ? "name" : "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                    return GetUserInfoFromToken(getNameType).FirstOrDefault().ObjectToString();
                }
            }

            return "";
        }

        public List<string> GetUserInfoFromToken(string ClaimType)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = "";

            token = GetToken();
            // token校验
            if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);

                return (from item in jwtToken.Claims
                        where item.Type == ClaimType
                        select item.Value).ToList();
            }

            return new List<string>() { };
        }

        public int ID => GetClaimValueByType("jti").FirstOrDefault().ObjectToInt();

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public List<string> GetClaimValueByType(string ClaimType)
        {
            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();
        }

        public string GetToken()
        {
            return _accessor.HttpContext?.Request?.Headers["Authorization"].ObjectToString().Replace("Bearer ", "");
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
