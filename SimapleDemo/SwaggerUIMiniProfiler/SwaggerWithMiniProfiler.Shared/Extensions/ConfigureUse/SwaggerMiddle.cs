/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse
*项目描述:
*类 名 称:SwaggerMiddle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 11:53:57
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse
{
    public static class SwaggerMiddle
    {
        public static void UseSwaggerModdle(this IApplicationBuilder app,Func<Stream> indexHtml)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerWithMiniProfiler.Api v1");
                c.SwaggerEndpoint($"https://petstore.swagger.io/v2/swagger.json", "SwaggerWithMiniProfiler.Api pet");
                if (indexHtml.Invoke() == null)
                {
                    var msg = "index.html的属性，必须设置为嵌入的资源";
                    throw new Exception(msg);
                }
                c.IndexStream = indexHtml;
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
