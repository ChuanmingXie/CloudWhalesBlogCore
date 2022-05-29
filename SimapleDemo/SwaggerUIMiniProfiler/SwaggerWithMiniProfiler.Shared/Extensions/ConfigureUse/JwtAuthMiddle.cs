/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse
*项目描述:
*类 名 称:JwtAuthMiddle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 18:32:51
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Http;
using SwaggerWithMiniProfiler.Model.ViewModel;
using SwaggerWithMiniProfiler.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse
{
    public class JwtAuthMiddle
    {
        /// <summary>
        /// Token验证授权中间件
        /// </summary>
        private readonly RequestDelegate _next;

        public JwtAuthMiddle(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;
            if (!headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }

            var tokenStr = headers["Authorization"];

            try
            {
                string jwtStr = tokenStr.ToString().Substring("Bearer".Length).Trim();
                // 验证缓存中是否窜在该jwt字符串
                if (!MemoryCaching.Exists(jwtStr))
                    return httpContext.Response.WriteAsync("非法情求");
                ModelToken modelToken = (ModelToken)MemoryCaching.Get(jwtStr);
                // 提取modelToken中的Sub属性进行author认证
                List<Claim> claims = new List<Claim>();
                Claim claim = new Claim(modelToken.Sub + "Type", modelToken.Sub);
                claims.Add(claim);
                ClaimsIdentity identity = new ClaimsIdentity(claims);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                httpContext.User = principal;
                return _next(httpContext);
            }
            catch (Exception)
            {
                return httpContext.Response.WriteAsync("token验证异常");
            }
        }

    }
}
