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
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Extensions.ExtensionConfigure
{
    public static class SwaggerMiddle
    {
        public static void UseSwaggerMiddle(this IApplicationBuilder app,Func<Stream> streamHtml)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
        }
    }
}
