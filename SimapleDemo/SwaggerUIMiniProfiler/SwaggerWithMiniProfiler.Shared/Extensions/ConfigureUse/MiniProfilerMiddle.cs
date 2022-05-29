/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse
*项目描述:
*类 名 称:MiniProfilerMiddle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 11:58:54
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Builder;
using System;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse
{
    public static class MiniProfilerMiddle
    {
        public static void UseMiniProfilerMiddle(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            app.UseMiniProfiler();
        }
    }
}
