/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Extensions.ExtensionConfigure
*项目描述:
*类 名 称:SwaggerMiddle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/25 23:51:07
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common;
using CloudWhalesBlogCore.Shared.NLogger;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CloudWhalesBlogCore.Extensions.ExtensionServices.CustomApiVersion;

namespace CloudWhalesBlogCore.Extensions.ExtensionConfigure
{
    public static class SwaggerMiddle
    {
        public static void UseSwaggerMiddle(this IApplicationBuilder app,Func<Stream> streamHtml)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var ApiName = AppSettings.app(new string[] { "Startup", "ApiName" });
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                c.SwaggerEndpoint($"https://petstore.swagger.io/v2/swagger.json", $"{ApiName} pet");

                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：{项目名.index.html}
                if (streamHtml.Invoke() == null)
                {
                    var msg = "index.html的属性，必须设置为嵌入的资源";
                    NLogHelper._.Error(msg);
                    throw new Exception(msg);
                }
                c.IndexStream = streamHtml;
                // 路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,
                // 注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，
                // 如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });
        }
    }
}
