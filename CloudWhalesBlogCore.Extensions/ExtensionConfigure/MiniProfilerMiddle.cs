/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Extensions.ExtensionConfigure
*项目描述:
*类 名 称:MiniProfilerMiddle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/26 15:22:23
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common;
using CloudWhalesBlogCore.Shared.Common.ConvertHelper;
using CloudWhalesBlogCore.Shared.NLogger;
using Microsoft.AspNetCore.Builder;
using System;

namespace CloudWhalesBlogCore.Extensions.ExtensionConfigure
{
    public static class MiniProfilerMiddle
    {
        public static void UseMiniProfilerMiddle(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            try
            {
                if (AppSettings.app("Startup", "MiniProfiler", "Enabled").ObjectToBool())
                {
                    app.UseMiniProfiler();
                }
            }
            catch (Exception ex)
            {
                NLogHelper._.Error($"开始执行MiniProfiler中间件时出现错误.\n{ex.Message}");
            }
        }
    }
}
